﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    /// <summary>
    /// Repository encargado de la gestión de la entidad RolForm en la base de datos.
    /// </summary>
    public class RolFormData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolFormData> _logger;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Instancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos.</param>
        /// <param name="logger">Instancia de <see cref="ILogger"/> para el registro de logs.</param>
        public RolFormData(ApplicationDbContext context, ILogger<RolFormData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los RolForms almacenados en la base de datos.
        /// </summary>
        /// <returns>Lista de RolForms.</returns>
        public async Task<IEnumerable<RolForm>> GetAllAsync()
        {
            return await _context.Set<RolForm>().ToListAsync();
        }

        /// <summary>
        /// Obtiene un RolForm por su ID.
        /// </summary>
        /// <param name="id">Identificador único del RolForm.</param>
        /// <returns>El RolForm con el ID especificado.</returns>
        public async Task<RolForm?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<RolForm>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener RolForm con ID {id}");
                throw; // Re-lanza la excepción para que sea manejada en capas superiores
            }
        }

        /// <summary>
        /// Crea un nuevo RolForm en la base de datos.
        /// </summary>
        /// <param name="rolForm">Instancia del RolForm a crear.</param>
        /// <returns>El RolForm creado.</returns>
        public async Task<RolForm> CreateAsync(RolForm rolForm)
        {
            try
            {
                await _context.Set<RolForm>().AddAsync(rolForm);
                await _context.SaveChangesAsync();
                return rolForm;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el RolForm {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza un RolForm existente en la base de datos.
        /// </summary>
        /// <param name="rolForm">Objeto con la información actualizada.</param>
        /// <returns>True si la operación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> UpdateAsync(RolForm rolForm)
        {
            try
            {
                _context.Set<RolForm>().Update(rolForm);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el RolForm {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un RolForm en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único del RolForm a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rolForm = await _context.Set<RolForm>().FindAsync(id);
                if (rolForm == null)
                    return false;

                _context.Set<RolForm>().Remove(rolForm);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el RolForm {ex.Message}");
                return false;
            }
        }
    }
}