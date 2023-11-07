using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ASSET_MANAGEMENTContext : DbContext
    {
        public ASSET_MANAGEMENTContext()
        {
        }

        public ASSET_MANAGEMENTContext(DbContextOptions<ASSET_MANAGEMENTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountsBalanceResume> AccountsBalanceResumes { get; set; } = null!;
        public virtual DbSet<AccountsResume> AccountsResumes { get; set; } = null!;
        public virtual DbSet<Activo> Activos { get; set; } = null!;
        public virtual DbSet<Asiento> Asientos { get; set; } = null!;
        public virtual DbSet<AsientoLinea> AsientoLineas { get; set; } = null!;
        public virtual DbSet<AuxiliarDepreciacion> AuxiliarDepreciacions { get; set; } = null!;
        public virtual DbSet<BitacoraErrore> BitacoraErrores { get; set; } = null!;
        public virtual DbSet<CategoriaCuentum> CategoriaCuenta { get; set; } = null!;
        public virtual DbSet<Clase> Clases { get; set; } = null!;
        public virtual DbSet<ClaseCuentum> ClaseCuenta { get; set; } = null!;
        public virtual DbSet<ClassesList> ClassesLists { get; set; } = null!;
        public virtual DbSet<ConciliacionBalanceClase> ConciliacionBalanceClases { get; set; } = null!;
        public virtual DbSet<CuentaContable> CuentaContables { get; set; } = null!;
        public virtual DbSet<EcuacionContable> EcuacionContables { get; set; } = null!;
        public virtual DbSet<Edificio> Edificios { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<ResumenActivo> ResumenActivos { get; set; } = null!;
        public virtual DbSet<ResumenActivosClase> ResumenActivosClases { get; set; } = null!;
        public virtual DbSet<ResumenAsiento> ResumenAsientos { get; set; } = null!;
        public virtual DbSet<ResumenAsientoLinea> ResumenAsientoLineas { get; set; } = null!;
        public virtual DbSet<ResumenUsuario> ResumenUsuarios { get; set; } = null!;
        public virtual DbSet<ResumenValidacionesCompleta> ResumenValidacionesCompletas { get; set; } = null!;
        public virtual DbSet<RiskAssetResume> RiskAssetResumes { get; set; } = null!;
        public virtual DbSet<TipoValidacion> TipoValidacions { get; set; } = null!;
        public virtual DbSet<Ubicacion> Ubicacions { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuarioRole> UsuarioRoles { get; set; } = null!;
        public virtual DbSet<Validacion> Validacions { get; set; } = null!;
        public virtual DbSet<ValidacionRiesgoActivo> ValidacionRiesgoActivos { get; set; } = null!;
        public virtual DbSet<ValidacionRiesgoClase> ValidacionRiesgoClases { get; set; } = null!;
        public virtual DbSet<ValidacionesResuman> ValidacionesResumen { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-S9ESODL;Initial Catalog = ASSET_MANAGEMENT; Persist Security Info =False; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountsBalanceResume>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ACCOUNTS_BALANCE_RESUME");

                entity.Property(e => e.Balance).HasColumnName("BALANCE");

                entity.Property(e => e.DescripcionCategoria)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CATEGORIA");

                entity.Property(e => e.DescripcionCuenta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CUENTA");

                entity.Property(e => e.IdCuenta)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ID_CUENTA");

                entity.Property(e => e.Naturaleza)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NATURALEZA");

                entity.Property(e => e.TotalCreditos).HasColumnName("TOTAL_CREDITOS");

                entity.Property(e => e.TotalDebitos).HasColumnName("TOTAL_DEBITOS");
            });

            modelBuilder.Entity<AccountsResume>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ACCOUNTS_RESUME");

                entity.Property(e => e.DescripcionCuenta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CUENTA");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.IdCuenta)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ID_CUENTA");
            });

            modelBuilder.Entity<Activo>(entity =>
            {
                entity.HasKey(e => e.IdActivo);

                entity.ToTable("ACTIVO");

                entity.Property(e => e.IdActivo).HasColumnName("ID_ACTIVO");

                entity.Property(e => e.DescripcionActivo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_ACTIVO");

                entity.Property(e => e.FechaAdquisicion)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_ADQUISICION");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.Property(e => e.IdDuenno).HasColumnName("ID_DUENNO");

                entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.IdUbicacion).HasColumnName("ID_UBICACION");

                entity.Property(e => e.PeriodosDepreciados)
                    .HasColumnName("PERIODOS_DEPRECIADOS")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValorAdquisicion).HasColumnName("VALOR_ADQUISICION");

                entity.HasOne(d => d.IdClaseNavigation)
                    .WithMany(p => p.Activos)
                    .HasForeignKey(d => d.IdClase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACTIVO_CLASE");

                entity.HasOne(d => d.IdDuennoNavigation)
                    .WithMany(p => p.Activos)
                    .HasForeignKey(d => d.IdDuenno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACTIVO_DUENNO");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Activos)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACTIVO_ESTADO");

                entity.HasOne(d => d.IdUbicacionNavigation)
                    .WithMany(p => p.Activos)
                    .HasForeignKey(d => d.IdUbicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACTIVO_UBICACION");
            });

            modelBuilder.Entity<Asiento>(entity =>
            {
                entity.HasKey(e => e.IdAsiento);

                entity.ToTable("ASIENTO");

                entity.Property(e => e.IdAsiento).HasColumnName("ID_ASIENTO");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.HasOne(d => d.IdClaseNavigation)
                    .WithMany(p => p.Asientos)
                    .HasForeignKey(d => d.IdClase)
                    .HasConstraintName("PK_ASIENTO_ACTIVO");
            });

            modelBuilder.Entity<AsientoLinea>(entity =>
            {
                entity.HasKey(e => e.IdAsientoLinea);

                entity.ToTable("ASIENTO_LINEA");

                entity.Property(e => e.IdAsientoLinea).HasColumnName("ID_ASIENTO_LINEA");

                entity.Property(e => e.Credito).HasColumnName("CREDITO");

                entity.Property(e => e.Debito).HasColumnName("DEBITO");

                entity.Property(e => e.DescripcionLinea)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_LINEA");

                entity.Property(e => e.IdAsiento).HasColumnName("ID_ASIENTO");

                entity.Property(e => e.IdCuentaContable)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ID_CUENTA_CONTABLE");

                entity.HasOne(d => d.IdAsientoNavigation)
                    .WithMany(p => p.AsientoLineas)
                    .HasForeignKey(d => d.IdAsiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ASIENTO_LINEA");

                entity.HasOne(d => d.IdCuentaContableNavigation)
                    .WithMany(p => p.AsientoLineas)
                    .HasForeignKey(d => d.IdCuentaContable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ASIENTO_CUENTA");
            });

            modelBuilder.Entity<AuxiliarDepreciacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AUXILIAR_DEPRECIACION");

                entity.Property(e => e.DepreciacionAcumulada).HasColumnName("DEPRECIACION_ACUMULADA");

                entity.Property(e => e.DepreciacionMensual).HasColumnName("DEPRECIACION_MENSUAL");

                entity.Property(e => e.DescripcionActivo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_ACTIVO");

                entity.Property(e => e.DescripcionClase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CLASE");

                entity.Property(e => e.FechaAdquisicion)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_ADQUISICION");

                entity.Property(e => e.IdActivo).HasColumnName("ID_ACTIVO");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.Property(e => e.PeriodosDepreciados).HasColumnName("PERIODOS_DEPRECIADOS");

                entity.Property(e => e.ValorAdquisicion).HasColumnName("VALOR_ADQUISICION");

                entity.Property(e => e.VidaUtil).HasColumnName("VIDA_UTIL");
            });

            modelBuilder.Entity<BitacoraErrore>(entity =>
            {
                entity.HasKey(e => e.IdError);

                entity.ToTable("BITACORA_ERRORES");

                entity.Property(e => e.IdError).HasColumnName("ID_ERROR");

                entity.Property(e => e.Error)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ERROR");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Pantalla)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PANTALLA");
            });

            modelBuilder.Entity<CategoriaCuentum>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.ToTable("CATEGORIA_CUENTA");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.DescripcionCategoria)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CATEGORIA");
            });

            modelBuilder.Entity<Clase>(entity =>
            {
                entity.HasKey(e => e.IdClase);

                entity.ToTable("CLASE");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.Property(e => e.DescripcionClase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CLASE");

                entity.Property(e => e.VidaUtil).HasColumnName("VIDA_UTIL");
            });

            modelBuilder.Entity<ClaseCuentum>(entity =>
            {
                entity.HasKey(e => e.IdClaseCuenta);

                entity.ToTable("CLASE_CUENTA");

                entity.Property(e => e.IdClaseCuenta).HasColumnName("ID_CLASE_CUENTA");

                entity.Property(e => e.IdCategoriaCuenta).HasColumnName("ID_CATEGORIA_CUENTA");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.Property(e => e.IdCuenta)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ID_CUENTA");

                entity.HasOne(d => d.IdCategoriaCuentaNavigation)
                    .WithMany(p => p.ClaseCuenta)
                    .HasForeignKey(d => d.IdCategoriaCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CATEGORIA_CLASECUENTA");

                entity.HasOne(d => d.IdClaseNavigation)
                    .WithMany(p => p.ClaseCuenta)
                    .HasForeignKey(d => d.IdClase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLASE_CLASECUENTA");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.ClaseCuenta)
                    .HasForeignKey(d => d.IdCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUENTA_CLASECUENTA");
            });

            modelBuilder.Entity<ClassesList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CLASSES_LIST");

                entity.Property(e => e.CuentaActivo)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CUENTA_ACTIVO");

                entity.Property(e => e.CuentaDepAcumulada)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CUENTA_DEP_ACUMULADA");

                entity.Property(e => e.CuentaGasto)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CUENTA_GASTO");

                entity.Property(e => e.DescripcionClase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CLASE");

                entity.Property(e => e.IdClase)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_CLASE");

                entity.Property(e => e.VidaUtil).HasColumnName("VIDA_UTIL");
            });

            modelBuilder.Entity<ConciliacionBalanceClase>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CONCILIACION_BALANCE_CLASE");

                entity.Property(e => e.Balance).HasColumnName("BALANCE");

                entity.Property(e => e.CategoriaCuenta)
                    .HasMaxLength(59)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORIA_CUENTA");

                entity.Property(e => e.DescripcionClase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CLASE");

                entity.Property(e => e.Diferencia).HasColumnName("DIFERENCIA");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.Property(e => e.IdCuenta)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ID_CUENTA");

                entity.Property(e => e.ValorAuxiliar).HasColumnName("VALOR_AUXILIAR");
            });

            modelBuilder.Entity<CuentaContable>(entity =>
            {
                entity.HasKey(e => e.IdCuenta);

                entity.ToTable("CUENTA_CONTABLE");

                entity.Property(e => e.IdCuenta)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ID_CUENTA");

                entity.Property(e => e.Balance)
                    .HasColumnName("BALANCE")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DescripcionCuenta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CUENTA");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.Naturaleza)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NATURALEZA");

                entity.Property(e => e.TotalCreditos)
                    .HasColumnName("TOTAL_CREDITOS")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalDebitos)
                    .HasColumnName("TOTAL_DEBITOS")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.CuentaContables)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUENTA_CATEGORIA");
            });

            modelBuilder.Entity<EcuacionContable>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ECUACION_CONTABLE");

                entity.Property(e => e.Categoria)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORIA");

                entity.Property(e => e.TotalBalance).HasColumnName("TOTAL_BALANCE");
            });

            modelBuilder.Entity<Edificio>(entity =>
            {
                entity.HasKey(e => e.IdEdificio);

                entity.ToTable("EDIFICIO");

                entity.Property(e => e.IdEdificio)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_EDIFICIO");

                entity.Property(e => e.DescripcionEdificio)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_EDIFICIO");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("ESTADO");

                entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.DescripcionEstado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_ESTADO");
            });

            modelBuilder.Entity<ResumenActivo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RESUMEN_ACTIVOS");

                entity.Property(e => e.DescripcionActivo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_ACTIVO");

                entity.Property(e => e.DescripcionClase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CLASE");

                entity.Property(e => e.DescripcionEstado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_ESTADO");

                entity.Property(e => e.DescripcionSeccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_SECCION");

                entity.Property(e => e.FechaAdquisicion)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_ADQUISICION");

                entity.Property(e => e.IdActivo).HasColumnName("ID_ACTIVO");

                entity.Property(e => e.IdEdificio)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_EDIFICIO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.PeriodosDepreciados).HasColumnName("PERIODOS_DEPRECIADOS");

                entity.Property(e => e.ValorAdquisicion).HasColumnName("VALOR_ADQUISICION");

                entity.Property(e => e.VidaUtil).HasColumnName("VIDA_UTIL");
            });

            modelBuilder.Entity<ResumenActivosClase>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RESUMEN_ACTIVOS_CLASE");

                entity.Property(e => e.DescripcionClase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CLASE");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.Property(e => e.TotalActivos).HasColumnName("TOTAL_ACTIVOS");
            });

            modelBuilder.Entity<ResumenAsiento>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RESUMEN_ASIENTOS");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.DescripcionClase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CLASE");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.IdAsiento)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_ASIENTO");

                entity.Property(e => e.TotalAsiento).HasColumnName("TOTAL_ASIENTO");
            });

            modelBuilder.Entity<ResumenAsientoLinea>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RESUMEN_ASIENTO_LINEAS");

                entity.Property(e => e.Balance).HasColumnName("BALANCE");

                entity.Property(e => e.Credito).HasColumnName("CREDITO");

                entity.Property(e => e.Debito).HasColumnName("DEBITO");

                entity.Property(e => e.DescripcionLinea)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_LINEA");

                entity.Property(e => e.IdAsientoLinea)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_ASIENTO_LINEA");

                entity.Property(e => e.IdCuentaContable)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ID_CUENTA_CONTABLE");
            });

            modelBuilder.Entity<ResumenUsuario>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RESUMEN_USUARIOS");

                entity.Property(e => e.Contrasenna)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONTRASENNA");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CORREO");

                entity.Property(e => e.Estado).HasColumnName("ESTADO");

                entity.Property(e => e.IdRole).HasColumnName("ID_ROLE");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.NombreRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_ROLE");
            });

            modelBuilder.Entity<ResumenValidacionesCompleta>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RESUMEN_VALIDACIONES_COMPLETAS");

                entity.Property(e => e.DescripcionActivo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_ACTIVO");

                entity.Property(e => e.DescripcionValidacion)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_VALIDACION");

                entity.Property(e => e.IdActivo).HasColumnName("ID_ACTIVO");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.Property(e => e.IdTipoValidacion).HasColumnName("ID_TIPO_VALIDACION");

                entity.Property(e => e.Valor)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("VALOR");
            });

            modelBuilder.Entity<RiskAssetResume>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RISK_ASSET_RESUME");

                entity.Property(e => e.AssetDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ASSET_DESCRIPTION");

                entity.Property(e => e.AssetId).HasColumnName("ASSET_ID");

                entity.Property(e => e.CompleteValidations).HasColumnName("COMPLETE_VALIDATIONS");

                entity.Property(e => e.CompletnessPercentaje).HasColumnName("COMPLETNESS_PERCENTAJE");

                entity.Property(e => e.RiskAssessment)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("RISK_ASSESSMENT");

                entity.Property(e => e.TotalValidations).HasColumnName("TOTAL_VALIDATIONS");
            });

            modelBuilder.Entity<TipoValidacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoValidacion)
                    .HasName("PK_VALIDACION");

                entity.ToTable("TIPO_VALIDACION");

                entity.Property(e => e.IdTipoValidacion).HasColumnName("ID_TIPO_VALIDACION");

                entity.Property(e => e.DescripcionValidacion)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_VALIDACION");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.HasOne(d => d.IdClaseNavigation)
                    .WithMany(p => p.TipoValidacions)
                    .HasForeignKey(d => d.IdClase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLASE_VALIDACION");
            });

            modelBuilder.Entity<Ubicacion>(entity =>
            {
                entity.HasKey(e => e.IdUbicacion);

                entity.ToTable("UBICACION");

                entity.Property(e => e.IdUbicacion).HasColumnName("ID_UBICACION");

                entity.Property(e => e.DescripcionSeccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_SECCION");

                entity.Property(e => e.IdEdificio)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_EDIFICIO");

                entity.HasOne(d => d.IdEdificioNavigation)
                    .WithMany(p => p.Ubicacions)
                    .HasForeignKey(d => d.IdEdificio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EDIFICIO_UBICACION");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.Contrasenna)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONTRASENNA");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CORREO");

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.EstadoContrasenna)
                    .HasColumnName("ESTADO_CONTRASENNA")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdRole).HasColumnName("ID_ROLE");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_USUARIO");
            });

            modelBuilder.Entity<UsuarioRole>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("USUARIO_ROLE");

                entity.Property(e => e.IdRole).HasColumnName("ID_ROLE");

                entity.Property(e => e.DescripcionPermisos)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_PERMISOS");

                entity.Property(e => e.NombreRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_ROLE");
            });

            modelBuilder.Entity<Validacion>(entity =>
            {
                entity.HasKey(e => e.IdValidacion)
                    .HasName("PK_VALIDACION_ACTIVO");

                entity.ToTable("VALIDACION");

                entity.Property(e => e.IdValidacion).HasColumnName("ID_VALIDACION");

                entity.Property(e => e.IdActivo).HasColumnName("ID_ACTIVO");

                entity.Property(e => e.IdTipoValidacion).HasColumnName("ID_TIPO_VALIDACION");

                entity.Property(e => e.Valor)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("VALOR");

                entity.HasOne(d => d.IdActivoNavigation)
                    .WithMany(p => p.Validacions)
                    .HasForeignKey(d => d.IdActivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VALIDACION_ACTIVO");

                entity.HasOne(d => d.IdTipoValidacionNavigation)
                    .WithMany(p => p.Validacions)
                    .HasForeignKey(d => d.IdTipoValidacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VALIDACION_TIPO");
            });

            modelBuilder.Entity<ValidacionRiesgoActivo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VALIDACION_RIESGO_ACTIVOS");

                entity.Property(e => e.DescripcionActivo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_ACTIVO");

                entity.Property(e => e.DescripcionClase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CLASE");

                entity.Property(e => e.IdActivo).HasColumnName("ID_ACTIVO");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.Property(e => e.IdDuenno).HasColumnName("ID_DUENNO");

                entity.Property(e => e.ValidacionRiesgo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALIDACION_RIESGO");

                entity.Property(e => e.ValidationPercentaje).HasColumnName("VALIDATION_PERCENTAJE");
            });

            modelBuilder.Entity<ValidacionRiesgoClase>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VALIDACION_RIESGO_CLASE");

                entity.Property(e => e.DescripcionClase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CLASE");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.Property(e => e.TotalActivos).HasColumnName("TOTAL_ACTIVOS");

                entity.Property(e => e.ValidacionRiesgo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALIDACION_RIESGO");
            });

            modelBuilder.Entity<ValidacionesResuman>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VALIDACIONES_RESUMEN");

                entity.Property(e => e.DescripcionClase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_CLASE");

                entity.Property(e => e.DescripcionValidacion)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_VALIDACION");

                entity.Property(e => e.IdClase).HasColumnName("ID_CLASE");

                entity.Property(e => e.IdTipoValidacion).HasColumnName("ID_TIPO_VALIDACION");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
