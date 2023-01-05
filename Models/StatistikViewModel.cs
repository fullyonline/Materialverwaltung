namespace Materialverwaltung.Models
{
    public class StatistikViewModel
    {
        public IDictionary<string, int> Data { get; set; }
        public StatistikViewModel(IDictionary<string, int> data)
        {
            Data = data;
        }
    }
}
