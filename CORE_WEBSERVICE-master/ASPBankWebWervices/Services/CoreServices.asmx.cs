using System.Web.Services;

namespace ASPBankWebWervices.Services
{
    [WebService(Namespace = "http://coreservices.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class CoreServices : System.Web.Services.WebService
    {
        private readonly IntegrationServices integration = new IntegrationServices();
        private readonly ATMServices atm = new ATMServices();
        private readonly InternetBankingServices internetBanking = new InternetBankingServices();

        public IntegrationServices Integration => integration;

        public ATMServices Atm => atm;

        public InternetBankingServices InternetBanking => internetBanking;

        //Insertar metodos de los servicios del core.
        public void CoreResponseClientRegister()
        {

        }

    }
}