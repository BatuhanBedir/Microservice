﻿namespace FreeCourse.Services.Catalog.Settings
{
    //options pattern: config dosyalarını bir nesneden okuma işlemi
    public interface IDatabaseSettings
    {
        public string CourseCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
