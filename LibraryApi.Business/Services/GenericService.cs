using LibraryApi.Business.Interfaces;
using LibraryApi.Business.Interfaces.Repositories;
using LibraryApi.Business.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.Services
{
    public class GenericService<T> : IGenericService<T> where T :class
    {
        protected readonly IGenericRepository<T> _genericRepository;
        protected readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<T> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _genericRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if(entity == null)
            {
                throw new KeyNotFoundException("Kayıt bulunamadı");
            }

            _genericRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _genericRepository.GetAllAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
             return await _genericRepository.GetByIdAsync(id);
         
        }

        public async Task UpdateAsync(T entity)
        {
            _genericRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
