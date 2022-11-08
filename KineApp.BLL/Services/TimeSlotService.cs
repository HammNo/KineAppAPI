using KineApp.BLL.DTO.TimeSlot;
using KineApp.BLL.DTO.User;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
using KineApp.BLL.Templates;
using KineApp.DL.Entities;
using KineApp.DL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMailer _mailer;

        public TimeSlotService(ITimeSlotRepository timeSlotRepository, IUserRepository userRepository, IMailer mailer, IAdminRepository adminRepository)
        {
            _timeSlotRepository = timeSlotRepository;
            _userRepository = userRepository;
            _mailer = mailer;
            _adminRepository = adminRepository;
        }

        public IEnumerable<TimeSlotDTO> GetAllWaiting()
        {
            return _timeSlotRepository.GetAllWaitingForConfirmation()
                                      .Where(t => t.Day.Date >= DateTime.Today)
                                      .Select(t => new TimeSlotDTO(t));
        }

        public void Register(TimeSlotRegistrationDTO command)
        {
            TimeSlot? timeSlot = _timeSlotRepository.GetWithUserAndDay(command.TimeSlotId);
            if (timeSlot == null || timeSlot.Day == null)
            {
                throw new TimeSlotException("Time slot not implemented");
            }
            if (timeSlot.Status != TimeSlotStatus.Free)
            {
                throw new TimeSlotException("Time slot already booked");
            }
            if (timeSlot.Day.Date.Add(timeSlot.StartTime) < DateTime.Now)
            {
                throw new TimeSlotException("To late to register");
            }
            if (!timeSlot.Day.Visible)
            {
                throw new TimeSlotException("Can't reach linked day");
            }
            User? user = _userRepository.FindOne(u => u.Id == command.UserId);
            if (user == null)
            {
                throw new UserException("Can't find user");
            }
            timeSlot.User = user;
            timeSlot.Status = TimeSlotStatus.Confirmed;
            timeSlot.Note = command.Note;
            _timeSlotRepository.Update(timeSlot);
        }

        public async Task Unregister(Guid id)
        {
            TimeSlot? timeSlot = _timeSlotRepository.GetWithUserAndDay(id);
            if (timeSlot == null || timeSlot.Day == null)
            {
                throw new TimeSlotException("Time slot not implemented");
            }
            await _mailer.Send(
                "Rendez-vous KineApp",
                MailTemplates.TSUnregister
                    .Replace("__date__", timeSlot.Day.Date.ToShortDateString())
                    .Replace("__startTime__", timeSlot.StartTime.ToString()),
                timeSlot.User.Email
            );
            timeSlot.User = null;
            timeSlot.Status = TimeSlotStatus.Free;
            timeSlot.Note = null;
            _timeSlotRepository.Update(timeSlot);
        }

        public Guid Remove(Guid id)
        {
            TimeSlot? timeSlot = _timeSlotRepository.GetWithUserAndDay(id);
            if (timeSlot == null)
            {
                throw new KeyNotFoundException();
            }
            if (timeSlot.User != null)
            {
                throw new TimeSlotException("Can't remove a booked time slot");
            }
            _timeSlotRepository.Remove(timeSlot);
            return id;
        }

        public async Task TryRegister(TimeSlotBookingDTO command, Guid userId)
        {
            TimeSlot? timeSlot = _timeSlotRepository.GetWithUserAndDay(command.TimeSlotId);
            if (timeSlot == null)
            {
                throw new KeyNotFoundException();
            }
            if (timeSlot.Status != TimeSlotStatus.Free)
            {
                throw new TimeSlotException("Time slot already booked");
            }
            if (timeSlot.Day.Date.Add(timeSlot.StartTime).AddHours(2) < DateTime.Now)
            {
                throw new TimeSlotException("To late to register");
            }
            if (!timeSlot.Day.Visible)
            {
                throw new TimeSlotException("Can't reach linked day");
            }
            User? user = _userRepository.FindOneWithAppointements(userId);
            if (user == null)
            {
                throw new UserException("Can't find user");
            }
            if (user.TimeSlots.Count > 3)
            {
                throw new UserException("User has already 3 appointements booked");
            }
            timeSlot.User = user;
            timeSlot.Status = TimeSlotStatus.WaitingForConfirmation;
            timeSlot.Note = command.Note;
            _timeSlotRepository.Update(timeSlot);
            IEnumerable<string>? adminMails = _adminRepository.GetAllMails();
            if (adminMails != null)
            {
                await _mailer.Send(
                    "Demande de rendez-vous KineApp",
                    MailTemplates.TSInformAdminRegistration
                        .Replace("__patientLName__", timeSlot.User.LastName)
                        .Replace("__patientFName__", timeSlot.User.FirstName)
                        .Replace("__requestNote__", timeSlot.Note)
                        .Replace("__patientEmail__", timeSlot.User.Email)
                        .Replace("__patientPhone__", timeSlot.User.PhoneNumber)
                        .Replace("__date__", timeSlot.Day.Date.ToShortDateString())
                        .Replace("__startTime__", timeSlot.StartTime.ToString()),
                    adminMails.ToArray()
                );
            }
            await _mailer.Send(
                "Demande de rendez-vous KineApp",
                MailTemplates.TSInformUserBooking
                    .Replace("__date__", timeSlot.Day.Date.ToShortDateString())
                    .Replace("__startTime__", timeSlot.StartTime.ToString()),
                timeSlot.User.Email
            );
        }

        public async Task ConfirmRegistration(Guid timeSlotId)
        {
            TimeSlot? timeSlot = _timeSlotRepository.GetWithUserAndDay(timeSlotId);
            if (timeSlot == null)
            {
                throw new KeyNotFoundException();
            }
            if (timeSlot.Status == TimeSlotStatus.Free)
            {
                throw new TimeSlotException("No booking to confirm");
            }
            if (timeSlot.Status == TimeSlotStatus.Confirmed)
            {
                throw new TimeSlotException("Booking already confirmed");
            }
            if (!timeSlot.Day.Visible)
            {
                throw new TimeSlotException("Can't reach linked day");
            }
            if (timeSlot.Day.Date.Add(timeSlot.StartTime) < DateTime.Now)
            {
                throw new TimeSlotException("To late to confirm");
            }
            if (timeSlot.User == null)
            {
                timeSlot.Status = TimeSlotStatus.Free;
                _timeSlotRepository.Update(timeSlot);
                throw new TimeSlotException("No user linked, time slot available to booking");
            }
            timeSlot.Status = TimeSlotStatus.Confirmed;
            _timeSlotRepository.Update(timeSlot);
            await _mailer.Send(
                "Demande de rendez-vous KineApp",
                MailTemplates.TSConfirmRegistration
                    .Replace("__date__", timeSlot.Day.Date.ToShortDateString())
                    .Replace("__startTime__", timeSlot.StartTime.ToString()),
                timeSlot.User.Email
            );
        }

        public async Task RejectRegistration(Guid timeSlotId)
        {
            TimeSlot? timeSlot = _timeSlotRepository.GetWithUserAndDay(timeSlotId);
            if (timeSlot == null)
            {
                throw new KeyNotFoundException();
            }
            if (timeSlot.Status == TimeSlotStatus.Free)
            {
                throw new TimeSlotException("No booking to reject");
            }
            if (timeSlot.Status == TimeSlotStatus.Confirmed)
            {
                throw new TimeSlotException("Booking already confirmed");
            }
            if (!timeSlot.Day.Visible)
            {
                throw new TimeSlotException("Can't reach linked day");
            }
            await _mailer.Send(
                "Demande de rendez-vous KineApp",
                MailTemplates.TSRejectRegistration
                    .Replace("__date__", timeSlot.Day.Date.ToShortDateString())
                    .Replace("__startTime__", timeSlot.StartTime.ToString()),
                timeSlot.User.Email
            );
            timeSlot.User = null;
            timeSlot.Note = null;
            timeSlot.Status = TimeSlotStatus.Free;
            _timeSlotRepository.Update(timeSlot);
        }
    }
}
