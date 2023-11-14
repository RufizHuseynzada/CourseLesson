﻿using AutoMapper;
using CourseLesson.Contract;
using CourseLesson.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseLesson.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private Course _context;

        public GenericRepository(Course context)
        {
            _context = context;
        }


        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var country = await GetAsync(id);

            return (country == null);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        { 
            if (id == null)
            {
                return null;
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
