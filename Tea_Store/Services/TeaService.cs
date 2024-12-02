using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tea_Store.Data;
using Tea_Store.Models;
using ViewModels.TeaContoller;

namespace Tea_Store.Services
{
    public interface ITeaService
    {
        Task<IEnumerable<TeaCatalogViewModel>> GetTeasAsync (
            string? searchQuery = null,
            string? category = null,
            string? sortOrder = "price_asc",
            int pageNumber = 1,
            int pageSize = 6);
    }
    public class TeaService : ITeaService
    {
        private readonly TeaDBContext _context;
        private readonly IMapper _mapper;

        public TeaService(TeaDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeaCatalogViewModel>> GetTeasAsync(
    string? searchQuery = null,
    string? category = null,
    string? sortOrder = "price_asc",
    int pageNumber = 1,
    int pageSize = 10)
        {
            try
            {
                var teas = _context.Teas.AsQueryable();

                teas = ApplySearch(teas, searchQuery);
                teas = ApplyCategoryFilter(teas, category);
                teas = ApplySorting(teas, sortOrder);
                teas = ApplyPagination(teas, pageNumber, pageSize);

                var teaList = await teas.ToListAsync(); // Тут може бути помилка
                return _mapper.Map<IEnumerable<TeaCatalogViewModel>>(teaList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching teas: {ex.Message}");
                throw; // Зберігаємо помилку для подальшої діагностики
            }
        }


        private IQueryable<Tea> ApplySearch(IQueryable<Tea> teas, string? searchQuery)
        {
            if (!string.IsNullOrEmpty(searchQuery))
                return teas.Where(t => t.Title.Contains(searchQuery));
            return teas;
        }

        private IQueryable<Tea> ApplyCategoryFilter(IQueryable<Tea> teas, string? category)
        {
            if (!string.IsNullOrEmpty(category))
                return teas.Where(t => t.Type == category);
            return teas;
        }

        private IQueryable<Tea> ApplySorting(IQueryable<Tea> teas, string? sortOrder)
        {
            return sortOrder switch
            {
                "price_asc" => teas.OrderBy(t => t.Price),
                "price_desc" => teas.OrderByDescending(t => t.Price),
                "rating_asc" => teas.OrderBy(t => t.Rating),
                "rating_desc" => teas.OrderByDescending(t => t.Rating),
                _ => teas.OrderBy(t => t.Price) // Значення за замовчуванням
            };
        }

        private IQueryable<Tea> ApplyPagination(IQueryable<Tea> teas, int pageNumber, int pageSize)
        {
            return teas.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
