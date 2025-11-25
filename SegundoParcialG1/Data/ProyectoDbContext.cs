using Microsoft.EntityFrameworkCore;
using SegundoParcialG1.Models;

namespace SegundoParcialG1.Data
{
    public class ProyectoDbContext : DbContext
    {
        public ProyectoDbContext(DbContextOptions<ProyectoDbContext> options) : base(options)
        { }

        public DbSet<Tarea> Tareas => Set<Tarea>();
        public DbSet<Miembro> Miembros => Set<Miembro>();
        public DbSet<Prioridad> Prioridades => Set<Prioridad>();
    }

