namespace Payment.Services
{
    public class PaymentServiceBase
    {
        internal ConfigService _configService;

        public PaymentServiceBase(int environment = 0)
        {
            SetEnvironment(environment);
        }

        public void SetEnvironment(int environment)
        {
            _configService = new ConfigService(environment);
        }
    }
}
