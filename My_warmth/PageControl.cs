using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace My_warmth
{
    public class PageControl
    {
        private static Frame AppFrame;
        private static HomePage homePage;
        public static Client client { get; set; }
        public static IndividualPerson person { get; set; }
        public static LegalPerson lPerson { get; set; }
        public static HomePage MainPage
        {
            get
            {
                if (homePage == null)
                    homePage = new HomePage(client, person);
                return homePage;
            }
        }
        private static HomePageLegal pageLegal;
        public static HomePageLegal mainLegal
        {
            get
            {
                if (pageLegal == null)
                    pageLegal = new HomePageLegal(client, lPerson);
                return pageLegal;
            }
        }

        private static Authorization authorization;
            public static Authorization AuthorizationPage
            {
                get
                {
                    if (authorization == null)
                    authorization = new Authorization();
                    return authorization;
                }
            }

        private static Registration registration;
        public static Registration RegistrationPage
        {
            get
            {
                if (registration == null)
                    registration = new Registration();
                return registration;
            }
        }

        private static RegistrationIndividual registrationIndividual;
        public static RegistrationIndividual RegistrationIndividualPage
        {
            get
            {
                if (registrationIndividual == null)
                    registrationIndividual = new RegistrationIndividual(AppFrame);
                return registrationIndividual;
            }
        }
        public static void SetAppFrame(Frame frame)
        { 
            AppFrame = frame;
        }

        private static RegistrationLegal registrationLegal;
        public static RegistrationLegal RegistrationLegalPage
        {
            get
            {
                if (registrationLegal == null)
                    registrationLegal = new RegistrationLegal(AppFrame);
                return registrationLegal;
            }
        }
    }
}
