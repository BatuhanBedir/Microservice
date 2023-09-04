﻿namespace FreeCourse.Web.Models
{
    public class ServiceApiSettings
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }
        public string PhotoStockUri { get; set; } //read
        public ServiceApi Catalog { get; set; }
        public ServiceApi PhotoStock { get; set; } //upload-delete
        public ServiceApi Basket { get; set; }
    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }
}
