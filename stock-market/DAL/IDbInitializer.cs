using System.Threading.Tasks;

public interface IDbInitializer
{
    Task Initialize();
    Task SeedData();
}
