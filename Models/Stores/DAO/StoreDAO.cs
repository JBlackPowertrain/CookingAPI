namespace BigCatCookinAPI.Models.Stores.DAO;

public class StoreDAO
{
    public Guid StoreId { get; set; }
    public string StoreName { get; set; }
    
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    
    //Street Num + street + building num
    public string Address { get; set; }
    public string ZipCode { get; set; }

    public bool IsClosed { get; set; }

    public bool IsPartner { get; set; }

}
