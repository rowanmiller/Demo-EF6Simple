using System.Data.Entity;

namespace CompletedDemo
{
    class MyConfig : DbConfiguration
    {
        public MyConfig()
        {
            SetPluralizationService(new MoreBetterEnglishPluralizationService());
            AddInterceptor(new NLogInterceptor());
        }
    }

}
