using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Templates
{
    public static class MailTemplates
    {

        public static string UserRegister = @"
            <h3Votre inscription sur la plateforme KineApp</h3>
            <div>
                <ul>
                    <li>Mail: __mail__</li>
                    <li>Nom: __lastname__</li>
                    <li>Prénom: __firstname__</li>
                </ul>
                <p>
                    Afin de compléter la démarche, veuillez cliquer sur le lien 
                    <a href=''>(pas encore implémenté)<a>
                </p>
            </div>
        ";

        public static string TSUnregister = @"
            <h3>Annulation de votre rdv</h3>
            <div>
                <p>
                    Votre rendez-vous du __date__ __startTime__ a été annulé.<br>
                    Pour plus d'informations, veuillez contacter l'administrateur.
                </p>
            </div>
        ";

        public static string TSRejectRegistration = @"
            <h3>Rejet de votre demande de rendez-vous</h3>
            <div>
                <p>
                    Votre demande de rendez-vous pour le __date__ __startTime__ a été rejetée.<br>
                    Pour plus d'informations, veuillez contacter l'administrateur.
                </p>
            </div>
        ";

        public static string TSConfirmRegistration = @"
            <h3>Acceptation de votre demande de rendez-vous</h3>
            <div>
                <p>
                    Votre demande de rendez-vous pour le __date__ __startTime__ a été acceptée.<br>
                </p>
            </div>
        ";


        public static string TSInformUserBooking = @"
            <h3>Votre demande de rendez-vous</h3>
            <div>
                <p>
                    Vous avez demandé un rendez-vous pour le __date__ __startTime__.<br>
                </p>
                <p>
                    Vous receverez prochainement un email vous informant de la confirmation du rendez-vous et des dispositions à prendre.
                </p>
            </div>
        ";

        public static string TSInformAdminRegistration = @"
            <h3>Nouvelle demande de rendez-vous</h3>
            <div>
                <p>
                    __patientLName__ __patientFName__ a demandé un rendez-vous pour le __date__ __startTime__.<br>
                    Le motif de la consultation est : __requestNote__
                </p>
                <p>
                    Informations relatives au patient :
                    <ul>
                        <li>Nom : __patientLName__</li>
                        <li>Prénom : __patientFName__</li>
                        <li>Email : __patientEmail__</li>
                        <li>Téléphone : __patientPhone__</li>
                    </ul>
                </p>
                <p>
                    Allez sur l'application de gestion pour confirmer ou infirmer la requête.
                </p>
            </div>
        ";
    }
}
