using ModelLayer.DTOs;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RepositoryLayer.Implementations
{
    public sealed class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private static readonly object _lock = new object();
        private static QuantityMeasurementCacheRepository? _instance;
        private List<MeasurementRecord> _cache = new List<MeasurementRecord>();
        private readonly string _filePath = "measurements.json";

        private QuantityMeasurementCacheRepository()
        {
            LoadFromDisk();
        }

        public static QuantityMeasurementCacheRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new QuantityMeasurementCacheRepository();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Save(MeasurementRecord record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            lock (_lock)
            {
                _cache.Add(record);
                SaveToDisk();
            }
        }

        public IEnumerable<MeasurementRecord> GetAll()
        {
            lock (_lock)
            {
                return _cache.ToList(); // Return a copy to avoid modification issues
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _cache.Clear();
                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }
            }
        }

        private void SaveToDisk()
        {
            try
            {
                var options = new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                };
                
                string jsonString = JsonSerializer.Serialize(_cache, options);
                File.WriteAllText(_filePath, jsonString);
            }
            catch (Exception ex)
            {
                // Log error but don't throw - we don't want to crash the app if disk write fails
                Console.WriteLine($"Warning: Failed to save to disk: {ex.Message}");
            }
        }

        private void LoadFromDisk()
        {
            if (!File.Exists(_filePath))
                return;

            try
            {
                lock (_lock)
                {
                    string jsonString = File.ReadAllText(_filePath);
                    var options = new JsonSerializerOptions 
                    { 
                        PropertyNameCaseInsensitive = true 
                    };
                    
                    var loaded = JsonSerializer.Deserialize<List<MeasurementRecord>>(jsonString, options);
                    if (loaded != null)
                    {
                        _cache = loaded;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error but don't throw - start with empty cache if file is corrupted
                Console.WriteLine($"Warning: Failed to load from disk: {ex.Message}");
                _cache = new List<MeasurementRecord>();
            }
        }
    }
}