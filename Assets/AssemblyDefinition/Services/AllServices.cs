namespace AssemblyDefinition.Services
{
  public class AllServices
  {
    private static AllServices _instance;
    public static AllServices Container => _instance ?? (_instance = new AllServices());

      // регистрация сервисов
    public void RegisterSingle<TService>(TService implementation) where TService : IService =>
      Implementation<TService>.ServiceInstance = implementation;

      //получаем зарегестрированный сервис
    public TService Single<TService>() where TService : IService =>
      Implementation<TService>.ServiceInstance;

      // класс для хранения сервисов
    private class Implementation<TService> where TService : IService
    {
      public static TService ServiceInstance;
    }
  }
}