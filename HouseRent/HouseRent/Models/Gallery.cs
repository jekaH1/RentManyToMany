namespace HouseRent.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        
        public List<GalleryImages> GalleryImages { get; set; }
    }
}
