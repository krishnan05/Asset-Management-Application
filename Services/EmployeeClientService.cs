using AssetManagement.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AssetManagementApp.Services
{
    public class EmployeeClientService : IEmployeeClientService
    {
        private readonly HttpClient _httpClient;
        private const string BaseApiUrl = "api/Employees";

        public EmployeeClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Employee>?> GetEmployeesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>(BaseApiUrl);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"{BaseApiUrl}/{id}");
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseApiUrl, employee);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseApiUrl}/{employee.Id}", employee);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseApiUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}