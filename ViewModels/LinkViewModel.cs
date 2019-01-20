namespace KinderKulturServer.ViewModels
{
   /// <summary>
   /// Link ViewModel
   /// </summary>
   public class LinkViewModel
   {
      public string Id { get; set; }
      public string title { get; set; }
      public string name { get; set; }
      public string desc { get; set; }
      public string url { get; set; }
      public string urldesc { get; set; }
      public string hostName { get; }
      public string imagename { get; set; }
   }
}