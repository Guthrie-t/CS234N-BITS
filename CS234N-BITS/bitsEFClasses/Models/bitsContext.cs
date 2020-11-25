using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bitsEFClasses.Models
{
    public partial class bitsContext : DbContext
    {
        public bitsContext()
        {
        }

        public bitsContext(DbContextOptions<bitsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<Adjunct> Adjunct { get; set; }
        public virtual DbSet<AdjunctType> AdjunctType { get; set; }
        public virtual DbSet<AppConfig> AppConfig { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Barrel> Barrel { get; set; }
        public virtual DbSet<Batch> Batch { get; set; }
        public virtual DbSet<BatchContainer> BatchContainer { get; set; }
        public virtual DbSet<BrewContainer> BrewContainer { get; set; }
        public virtual DbSet<ContainerSize> ContainerSize { get; set; }
        public virtual DbSet<ContainerStatus> ContainerStatus { get; set; }
        public virtual DbSet<ContainerType> ContainerType { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Fermentable> Fermentable { get; set; }
        public virtual DbSet<FermentableType> FermentableType { get; set; }
        public virtual DbSet<Hop> Hop { get; set; }
        public virtual DbSet<HopType> HopType { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<IngredientInventoryAddition> IngredientInventoryAddition { get; set; }
        public virtual DbSet<IngredientInventorySubtraction> IngredientInventorySubtraction { get; set; }
        public virtual DbSet<IngredientSubstitute> IngredientSubstitute { get; set; }
        public virtual DbSet<IngredientType> IngredientType { get; set; }
        public virtual DbSet<IngredientUsedIn> IngredientUsedIn { get; set; }
        public virtual DbSet<InventoryTransaction> InventoryTransaction { get; set; }
        public virtual DbSet<InventoryTransactionType> InventoryTransactionType { get; set; }
        public virtual DbSet<Mash> Mash { get; set; }
        public virtual DbSet<MashStep> MashStep { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductContainerInventory> ProductContainerInventory { get; set; }
        public virtual DbSet<ProductContainerSize> ProductContainerSize { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        public virtual DbSet<Style> Style { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SupplierAddress> SupplierAddress { get; set; }
        public virtual DbSet<UnitType> UnitType { get; set; }
        public virtual DbSet<UseDuring> UseDuring { get; set; }
        public virtual DbSet<Yeast> Yeast { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigDB.GetMySqlConnectionString();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasColumnName("contact_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SalesPersonName)
                    .HasColumnName("sales_person_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .HasColumnName("zipcode")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StreetLine1)
                    .IsRequired()
                    .HasColumnName("street_line_1")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StreetLine2)
                    .HasColumnName("street_line_2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .HasColumnName("zipcode")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.ToTable("address_type");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.AddressTypeId).HasColumnName("address_type_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Adjunct>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("PRIMARY");

                entity.ToTable("adjunct");

                entity.HasIndex(e => e.AdjunctTypeId)
                    .HasName("adjunct_adjunct_type_FK_idx");

                entity.HasIndex(e => e.RecommendedUseDuringId)
                    .HasName("adjunct_use_during_FK_idx");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.AdjunctTypeId).HasColumnName("adjunct_type_id");

                entity.Property(e => e.BatchVolume)
                    .HasColumnName("batch_volume")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RecommendedQuantity)
                    .HasColumnName("recommended_quantity")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RecommendedUseDuringId).HasColumnName("recommended_use_during_id");

                entity.Property(e => e.UseFor)
                    .HasColumnName("use_for")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.AdjunctType)
                    .WithMany(p => p.Adjunct)
                    .HasForeignKey(d => d.AdjunctTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("adjunct_adjunct_type_FK");

                entity.HasOne(d => d.Ingredient)
                    .WithOne(p => p.Adjunct)
                    .HasForeignKey<Adjunct>(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("adjunct_ingredient_FK");

                entity.HasOne(d => d.RecommendedUseDuring)
                    .WithMany(p => p.Adjunct)
                    .HasForeignKey(d => d.RecommendedUseDuringId)
                    .HasConstraintName("adjunct_use_during_FK");
            });

            modelBuilder.Entity<AdjunctType>(entity =>
            {
                entity.ToTable("adjunct_type");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.AdjunctTypeId).HasColumnName("adjunct_type_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppConfig>(entity =>
            {
                entity.HasKey(e => e.BreweryId)
                    .HasName("PRIMARY");

                entity.ToTable("app_config");

                entity.Property(e => e.BreweryId).HasColumnName("brewery_id");

                entity.Property(e => e.BreweryLogo)
                    .HasColumnName("brewery_logo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BreweryName)
                    .IsRequired()
                    .HasColumnName("brewery_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Color1)
                    .HasColumnName("color_1")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Color2)
                    .HasColumnName("color_2")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Color3)
                    .HasColumnName("color_3")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ColorBlack)
                    .HasColumnName("color_black")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ColorWhite)
                    .HasColumnName("color_white")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultUnits)
                    .IsRequired()
                    .HasColumnName("default_units")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'metric'");

                entity.Property(e => e.HomePageBackgroundImage)
                    .HasColumnName("home_page_background_image")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePageText)
                    .HasColumnName("home_page_text")
                    .HasMaxLength(5000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("app_user");

                entity.Property(e => e.AppUserId).HasColumnName("app_user_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Barrel>(entity =>
            {
                entity.HasKey(e => e.BrewContainerId)
                    .HasName("PRIMARY");

                entity.ToTable("barrel");

                entity.Property(e => e.BrewContainerId).HasColumnName("brew_container_id");

                entity.Property(e => e.Treatment)
                    .IsRequired()
                    .HasColumnName("treatment")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BrewContainer)
                    .WithOne(p => p.Barrel)
                    .HasForeignKey<Barrel>(d => d.BrewContainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("barrel_brew_container_FK");
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("batch");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("batch_recipe_FK_idx");

                entity.HasIndex(e => e.RecipeId)
                    .HasName("batch_recipe_FK");

                entity.Property(e => e.BatchId).HasColumnName("batch_id");

                entity.Property(e => e.Abv).HasColumnName("abv");

                entity.Property(e => e.ActualEfficiency).HasColumnName("actual_efficiency");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Calories).HasColumnName("calories");

                entity.Property(e => e.Carbonation).HasColumnName("carbonation");

                entity.Property(e => e.CarbonationTemp).HasColumnName("carbonation_temp");

                entity.Property(e => e.CarbonationUsed)
                    .HasColumnName("carbonation_used")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.EstimatedFinishDate).HasColumnName("estimated_finish_date");

                entity.Property(e => e.FermentationStages).HasColumnName("fermentation_stages");

                entity.Property(e => e.Fg).HasColumnName("fg");

                entity.Property(e => e.FinishDate).HasColumnName("finish_date");

                entity.Property(e => e.ForcedCarbonation).HasColumnName("forced_carbonation");

                entity.Property(e => e.Ibu).HasColumnName("ibu");

                entity.Property(e => e.IbuMethod)
                    .HasColumnName("ibu_method")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.KegPrimingFactor).HasColumnName("keg_priming_factor");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Og).HasColumnName("og");

                entity.Property(e => e.PrimaryAge).HasColumnName("primary_age");

                entity.Property(e => e.PrimaryTemp).HasColumnName("primary_temp");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.ScheduledStartDate).HasColumnName("scheduled_start_date");

                entity.Property(e => e.SecondaryAge).HasColumnName("secondary_age");

                entity.Property(e => e.SecondaryTemp).HasColumnName("secondary_temp");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.TasteNotes)
                    .HasColumnName("taste_notes")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.TasteRating).HasColumnName("taste_rating");

                entity.Property(e => e.Temp).HasColumnName("temp");

                entity.Property(e => e.TertiaryAge).HasColumnName("tertiary_age");

                entity.Property(e => e.UnitCost)
                    .HasColumnName("unit_cost")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Volume).HasColumnName("volume");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.Batch)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("batch_equipment_FK");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Batch)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("batch_recipe_FK");
            });

            modelBuilder.Entity<BatchContainer>(entity =>
            {
                entity.HasKey(e => new { e.BatchId, e.BrewContainerId })
                    .HasName("PRIMARY");

                entity.ToTable("batch_container");

                entity.HasIndex(e => e.BrewContainerId)
                    .HasName("batch_container_brew_container_FK_idx");

                entity.Property(e => e.BatchId).HasColumnName("batch_id");

                entity.Property(e => e.BrewContainerId).HasColumnName("brew_container_id");

                entity.Property(e => e.DateIn).HasColumnName("date_in");

                entity.Property(e => e.DateOut).HasColumnName("date_out");

                entity.Property(e => e.Volume).HasColumnName("volume");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchContainer)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("batch_container_batch_FK");

                entity.HasOne(d => d.BrewContainer)
                    .WithMany(p => p.BatchContainer)
                    .HasForeignKey(d => d.BrewContainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("batch_container_brew_container_FK");
            });

            modelBuilder.Entity<BrewContainer>(entity =>
            {
                entity.ToTable("brew_container");

                entity.HasIndex(e => e.ContainerSizeId)
                    .HasName("brew_container_container_size_idx");

                entity.HasIndex(e => e.ContainerStatusId)
                    .HasName("brew_container_container_status_FK_idx");

                entity.HasIndex(e => e.ContainerTypeId)
                    .HasName("brew_container_container_type_FK_idx");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.BrewContainerId).HasColumnName("brew_container_id");

                entity.Property(e => e.ContainerSizeId).HasColumnName("container_size_id");

                entity.Property(e => e.ContainerStatusId).HasColumnName("container_status_id");

                entity.Property(e => e.ContainerTypeId).HasColumnName("container_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ContainerSize)
                    .WithMany(p => p.BrewContainer)
                    .HasForeignKey(d => d.ContainerSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brew_container_container_size");

                entity.HasOne(d => d.ContainerStatus)
                    .WithMany(p => p.BrewContainer)
                    .HasForeignKey(d => d.ContainerStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brew_container_container_status_FK");

                entity.HasOne(d => d.ContainerType)
                    .WithMany(p => p.BrewContainer)
                    .HasForeignKey(d => d.ContainerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brew_container_container_type_FK");
            });

            modelBuilder.Entity<ContainerSize>(entity =>
            {
                entity.ToTable("container_size");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ContainerSizeId).HasColumnName("container_size_id");

                entity.Property(e => e.MaxVolume).HasColumnName("max_volume");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkingVolume).HasColumnName("working_volume");
            });

            modelBuilder.Entity<ContainerStatus>(entity =>
            {
                entity.ToTable("container_status");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ContainerStatusId).HasColumnName("container_status_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ContainerType>(entity =>
            {
                entity.ToTable("container_type");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ContainerTypeId).HasColumnName("container_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.ToTable("equipment");

                entity.HasIndex(e => e.Name)
                    .HasName("equipment_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.BatchSize).HasColumnName("batch_size");

                entity.Property(e => e.BoilSize).HasColumnName("boil_size");

                entity.Property(e => e.BoilTime).HasColumnName("boil_time");

                entity.Property(e => e.CalcBoilVolume).HasColumnName("calc_boil_volume");

                entity.Property(e => e.CoolingLossPct).HasColumnName("cooling_loss_pct");

                entity.Property(e => e.EvapRate).HasColumnName("evap_rate");

                entity.Property(e => e.HopUtilization).HasColumnName("hop_utilization");

                entity.Property(e => e.LauterDeadspace).HasColumnName("lauter_deadspace");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.TopUpKettle).HasColumnName("top_up_kettle");

                entity.Property(e => e.TopUpWater).HasColumnName("top_up_water");

                entity.Property(e => e.TrubChillerLoss).HasColumnName("trub_chiller_loss");

                entity.Property(e => e.TunSpecificHeat).HasColumnName("tun_specific_heat");

                entity.Property(e => e.TunVolume).HasColumnName("tun_volume");

                entity.Property(e => e.TunWeight).HasColumnName("tun_weight");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<Fermentable>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("PRIMARY");

                entity.ToTable("fermentable");

                entity.HasIndex(e => e.FermentableTypeId)
                    .HasName("fermentable_fermentable_type_FK_idx");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.AddAfterBoil).HasColumnName("add_after_boil");

                entity.Property(e => e.CoarseFineDiff).HasColumnName("coarse_fine_diff");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.DiastaticPower).HasColumnName("diastatic_power");

                entity.Property(e => e.FermentableTypeId).HasColumnName("fermentable_type_id");

                entity.Property(e => e.IbuGalPerLb).HasColumnName("ibu_gal_per_lb");

                entity.Property(e => e.MaxInBatch).HasColumnName("max_in_batch");

                entity.Property(e => e.Moisture).HasColumnName("moisture");

                entity.Property(e => e.Origin)
                    .HasColumnName("origin")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Potential).HasColumnName("potential");

                entity.Property(e => e.Protein).HasColumnName("protein");

                entity.Property(e => e.RecommendMash).HasColumnName("recommend_mash");

                entity.Property(e => e.Yield).HasColumnName("yield");

                entity.HasOne(d => d.FermentableType)
                    .WithMany(p => p.Fermentable)
                    .HasForeignKey(d => d.FermentableTypeId)
                    .HasConstraintName("fermentable_fermentable_type_FK");

                entity.HasOne(d => d.Ingredient)
                    .WithOne(p => p.Fermentable)
                    .HasForeignKey<Fermentable>(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fermentable_ingredient_FK");
            });

            modelBuilder.Entity<FermentableType>(entity =>
            {
                entity.ToTable("fermentable_type");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.FermentableTypeId).HasColumnName("fermentable_type_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Hop>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("PRIMARY");

                entity.ToTable("hop");

                entity.HasIndex(e => e.HopTypeId)
                    .HasName("hop_hop_type_idx");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.Alpha).HasColumnName("alpha");

                entity.Property(e => e.Beta).HasColumnName("beta");

                entity.Property(e => e.Form)
                    .HasColumnName("form")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HopTypeId).HasColumnName("hop_type_id");

                entity.Property(e => e.Hsi).HasColumnName("hsi");

                entity.Property(e => e.Origin)
                    .HasColumnName("origin")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.HopType)
                    .WithMany(p => p.Hop)
                    .HasForeignKey(d => d.HopTypeId)
                    .HasConstraintName("hop_hop_type");

                entity.HasOne(d => d.Ingredient)
                    .WithOne(p => p.Hop)
                    .HasForeignKey<Hop>(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hop_ingredient_FK");
            });

            modelBuilder.Entity<HopType>(entity =>
            {
                entity.ToTable("hop_type");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.HopTypeId).HasColumnName("hop_type_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("ingredient");

                entity.HasIndex(e => e.IngredientTypeId)
                    .HasName("ingredient_ingredient_type_FK_idx");

                entity.HasIndex(e => e.UnitTypeId)
                    .HasName("ingredient_unit_type_FK_idx");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.IngredientTypeId).HasColumnName("ingredient_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.OnHandQuantity).HasColumnName("on_hand_quantity");

                entity.Property(e => e.ReorderPoint).HasColumnName("reorder_point");

                entity.Property(e => e.UnitCost)
                    .HasColumnName("unit_cost")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.UnitTypeId).HasColumnName("unit_type_id");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.IngredientType)
                    .WithMany(p => p.Ingredient)
                    .HasForeignKey(d => d.IngredientTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_ingredient_type_FK");

                entity.HasOne(d => d.UnitType)
                    .WithMany(p => p.Ingredient)
                    .HasForeignKey(d => d.UnitTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_unit_type_FK");
            });

            modelBuilder.Entity<IngredientInventoryAddition>(entity =>
            {
                entity.ToTable("ingredient_inventory_addition");

                entity.HasIndex(e => e.IngredientId)
                    .HasName("ingredient_inventory_addition_ingredient_FK_idx");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("ingredient_invertory_addition_supplier_FK_idx");

                entity.Property(e => e.IngredientInventoryAdditionId).HasColumnName("ingredient_inventory_addition_id");

                entity.Property(e => e.EstimatedDeliveryDate).HasColumnName("estimated_delivery_date");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.OrderDate).HasColumnName("order_date");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.QuantityRemaining).HasColumnName("quantity_remaining");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.TransactionDate).HasColumnName("transaction_date");

                entity.Property(e => e.UnitCost)
                    .HasColumnName("unit_cost")
                    .HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.IngredientInventoryAddition)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_inventory_addition_ingredient_FK");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.IngredientInventoryAddition)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_invertory_addition_supplier_FK");
            });

            modelBuilder.Entity<IngredientInventorySubtraction>(entity =>
            {
                entity.ToTable("ingredient_inventory_subtraction");

                entity.HasIndex(e => e.BatchId)
                    .HasName("ingredient_purchased_batch_FK");

                entity.HasIndex(e => e.IngredientId)
                    .HasName("ingredient_inventory_subtraction_ingredient_FK");

                entity.Property(e => e.IngredientInventorySubtractionId).HasColumnName("ingredient_inventory_subtraction_id");

                entity.Property(e => e.BatchId).HasColumnName("batch_id");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnName("reason")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnName("transaction_date");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.IngredientInventorySubtraction)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("ingredient_purchased_batch_FK");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.IngredientInventorySubtraction)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_inventory_subtraction_ingredient_FK");
            });

            modelBuilder.Entity<IngredientSubstitute>(entity =>
            {
                entity.HasKey(e => new { e.IngredientId, e.SubstituteIngredientId })
                    .HasName("PRIMARY");

                entity.ToTable("ingredient_substitute");

                entity.HasIndex(e => e.SubstituteIngredientId)
                    .HasName("ingredient_substitute_substitute_ingredient_FK_idx");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.SubstituteIngredientId).HasColumnName("substitute_ingredient_id");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.IngredientSubstituteIngredient)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_substitute_ingredient_FK");

                entity.HasOne(d => d.SubstituteIngredient)
                    .WithMany(p => p.IngredientSubstituteSubstituteIngredient)
                    .HasForeignKey(d => d.SubstituteIngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_substitute_substitute_ingredient_FK");
            });

            modelBuilder.Entity<IngredientType>(entity =>
            {
                entity.ToTable("ingredient_type");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IngredientTypeId).HasColumnName("ingredient_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IngredientUsedIn>(entity =>
            {
                entity.HasKey(e => new { e.IngredientId, e.StyleId })
                    .HasName("PRIMARY");

                entity.ToTable("ingredient_used_in");

                entity.HasIndex(e => e.StyleId)
                    .HasName("usedin_style_type_FK_idx");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.StyleId).HasColumnName("style_id");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.IngredientUsedIn)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("used_in_ingredient_FK");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.IngredientUsedIn)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("used_in_style_type_FK");
            });

            modelBuilder.Entity<InventoryTransaction>(entity =>
            {
                entity.ToTable("inventory_transaction");

                entity.HasIndex(e => e.AccountId)
                    .HasName("inventory_transaction_account_idx");

                entity.HasIndex(e => e.AppUserId)
                    .HasName("inventory_transaction_app_user_FK_idx");

                entity.HasIndex(e => e.BatchId)
                    .HasName("inventory_transaction_batch_FK_idx");

                entity.HasIndex(e => e.InventoryTransctionTypeId)
                    .HasName("inventory_transaction_transaction_type_FK_idx");

                entity.HasIndex(e => e.ProductContainerSizeId)
                    .HasName("inventory_transaction_product_container_size_FK_idx");

                entity.Property(e => e.InventoryTransactionId).HasColumnName("inventory_transaction_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.AppUserId).HasColumnName("app_user_id");

                entity.Property(e => e.BatchId).HasColumnName("batch_id");

                entity.Property(e => e.InventoryTransactionDate).HasColumnName("inventory_transaction_date");

                entity.Property(e => e.InventoryTransctionTypeId).HasColumnName("inventory_transction_type_id");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductContainerSizeId).HasColumnName("product_container_size_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.InventoryTransaction)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_account");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.InventoryTransaction)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_app_user_FK");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.InventoryTransaction)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_batch_FK");

                entity.HasOne(d => d.InventoryTransctionType)
                    .WithMany(p => p.InventoryTransaction)
                    .HasForeignKey(d => d.InventoryTransctionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_transaction_type_FK");

                entity.HasOne(d => d.ProductContainerSize)
                    .WithMany(p => p.InventoryTransaction)
                    .HasForeignKey(d => d.ProductContainerSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_product_container_size_FK");
            });

            modelBuilder.Entity<InventoryTransactionType>(entity =>
            {
                entity.ToTable("inventory_transaction_type");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.InventoryTransactionTypeId).HasColumnName("inventory_transaction_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mash>(entity =>
            {
                entity.ToTable("mash");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.MashId).HasColumnName("mash_id");

                entity.Property(e => e.EquipmentAdjust).HasColumnName("equipment_adjust");

                entity.Property(e => e.GrainTemp).HasColumnName("grain_temp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Ph).HasColumnName("ph");

                entity.Property(e => e.SpargeTemp).HasColumnName("sparge_temp");

                entity.Property(e => e.TunSpecificHeat).HasColumnName("tun_specific_heat");

                entity.Property(e => e.TunTemp).HasColumnName("tun_temp");

                entity.Property(e => e.TunWeight).HasColumnName("tun_weight");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<MashStep>(entity =>
            {
                entity.ToTable("mash_step");

                entity.HasIndex(e => e.MashId)
                    .HasName("mast_step_mash_FK_idx");

                entity.Property(e => e.MashStepId).HasColumnName("mash_step_id");

                entity.Property(e => e.DecoctionAmount)
                    .HasColumnName("decoction_amount")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.EndTemp).HasColumnName("end_temp");

                entity.Property(e => e.InfuseAmount).HasColumnName("infuse_amount");

                entity.Property(e => e.InfuseTemp)
                    .HasColumnName("infuse_temp")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MashId).HasColumnName("mash_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RampTime).HasColumnName("ramp_time");

                entity.Property(e => e.StepTemp).HasColumnName("step_temp");

                entity.Property(e => e.StepTime).HasColumnName("step_time");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Version).HasColumnName("version");

                entity.Property(e => e.WaterGrainRatio)
                    .HasColumnName("water_grain_ratio")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Mash)
                    .WithMany(p => p.MashStep)
                    .HasForeignKey(d => d.MashId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mast_step_mash_FK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => new { e.BatchId, e.ProductContainerSizeId })
                    .HasName("PRIMARY");

                entity.ToTable("product");

                entity.HasIndex(e => e.BatchId)
                    .HasName("keg_batch_FK_idx");

                entity.HasIndex(e => e.ProductContainerSizeId)
                    .HasName("product_product_container_size_FK");

                entity.Property(e => e.BatchId).HasColumnName("batch_id");

                entity.Property(e => e.ProductContainerSizeId).HasColumnName("product_container_size_id");

                entity.Property(e => e.IngredientCost)
                    .HasColumnName("ingredient_cost")
                    .HasColumnType("decimal(10,4)");

                entity.Property(e => e.QuantityRacked).HasColumnName("quantity_racked");

                entity.Property(e => e.QuantityRemaining).HasColumnName("quantity_remaining");

                entity.Property(e => e.RackedDate).HasColumnName("racked_date");

                entity.Property(e => e.SellByDate).HasColumnName("sell_by_date");

                entity.Property(e => e.SuggestedPrice)
                    .HasColumnName("suggested_price")
                    .HasColumnType("decimal(10,4)");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_batch_FK");

                entity.HasOne(d => d.ProductContainerSize)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductContainerSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_product_container_size_FK");
            });

            modelBuilder.Entity<ProductContainerInventory>(entity =>
            {
                entity.HasKey(e => e.ContainerSizeId)
                    .HasName("PRIMARY");

                entity.ToTable("product_container_inventory");

                entity.Property(e => e.ContainerSizeId).HasColumnName("container_size_id");

                entity.Property(e => e.QuantityClean).HasColumnName("quantity_clean");

                entity.Property(e => e.QuantityDirty).HasColumnName("quantity_dirty");

                entity.HasOne(d => d.ContainerSize)
                    .WithOne(p => p.ProductContainerInventory)
                    .HasForeignKey<ProductContainerInventory>(d => d.ContainerSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_container_inventory_product_container_FK");
            });

            modelBuilder.Entity<ProductContainerSize>(entity =>
            {
                entity.HasKey(e => e.ContainerSizeId)
                    .HasName("PRIMARY");

                entity.ToTable("product_container_size");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ContainerSizeId).HasColumnName("container_size_id");

                entity.Property(e => e.ItemQuantity).HasColumnName("item_quantity");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Volume).HasColumnName("volume");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("recipe");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("recipe_equipment_FK_idx");

                entity.HasIndex(e => e.MashId)
                    .HasName("recipe_mash_FK_idx");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StyleId)
                    .HasName("recipe_style_type_FK_idx");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.ActualEfficiency)
                    .HasColumnName("actual_efficiency")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BoilTime)
                    .HasColumnName("boil_time")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BoilVolume)
                    .HasColumnName("boil_volume")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Brewer)
                    .IsRequired()
                    .HasColumnName("brewer")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CarbonationTemp).HasColumnName("carbonation_temp");

                entity.Property(e => e.CarbonationUsed)
                    .HasColumnName("carbonation_used")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Efficiency)
                    .HasColumnName("efficiency")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.EstimatedAbv)
                    .HasColumnName("estimated_abv")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EstimatedColor)
                    .HasColumnName("estimated_color")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EstimatedFg)
                    .HasColumnName("estimated_fg")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EstimatedOg)
                    .HasColumnName("estimated_og")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.FermentationStages)
                    .HasColumnName("fermentation_stages")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ForcedCarbonation).HasColumnName("forced_carbonation");

                entity.Property(e => e.KegPrimingFactor).HasColumnName("keg_priming_factor");

                entity.Property(e => e.MashId).HasColumnName("mash_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StyleId).HasColumnName("style_id");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.Property(e => e.Volume).HasColumnName("volume");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.Recipe)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("recipe_equipment_FK");

                entity.HasOne(d => d.Mash)
                    .WithMany(p => p.Recipe)
                    .HasForeignKey(d => d.MashId)
                    .HasConstraintName("recipe_mash_FK");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.Recipe)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_style_FK");
            });

            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.ToTable("recipe_ingredient");

                entity.HasIndex(e => e.IngredientId)
                    .HasName("recipe_ingredient_ingredient_idx");

                entity.HasIndex(e => e.RecipeId)
                    .HasName("recipe_ingredient_recipe_FK");

                entity.HasIndex(e => e.UseDuringId)
                    .HasName("recipe_ingredient_use_during_FK_idx");

                entity.Property(e => e.RecipeIngredientId).HasColumnName("recipe_ingredient_id");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UseDuringId).HasColumnName("use_during_id");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RecipeIngredient)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_ingredient_ingredient_FK");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeIngredient)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_ingredient_recipe_FK");

                entity.HasOne(d => d.UseDuring)
                    .WithMany(p => p.RecipeIngredient)
                    .HasForeignKey(d => d.UseDuringId)
                    .HasConstraintName("recipe_ingredient_use_during_FK");
            });

            modelBuilder.Entity<Style>(entity =>
            {
                entity.ToTable("style");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.StyleId).HasColumnName("style_id");

                entity.Property(e => e.AbvMax).HasColumnName("abv_max");

                entity.Property(e => e.AbvMin).HasColumnName("abv_min");

                entity.Property(e => e.CarbMax).HasColumnName("carb_max");

                entity.Property(e => e.CarbMin).HasColumnName("carb_min");

                entity.Property(e => e.CategoryLetter)
                    .HasColumnName("category_letter")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryName)
                    .HasColumnName("category_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryNumber)
                    .HasColumnName("category_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ColorMax).HasColumnName("color_max");

                entity.Property(e => e.ColorMin).HasColumnName("color_min");

                entity.Property(e => e.Examples)
                    .HasColumnName("examples")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FgMax).HasColumnName("fg_max");

                entity.Property(e => e.FgMin).HasColumnName("fg_min");

                entity.Property(e => e.IbuMax).HasColumnName("ibu_max");

                entity.Property(e => e.IbuMin).HasColumnName("ibu_min");

                entity.Property(e => e.Ingredients)
                    .HasColumnName("ingredients")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.OgMax).HasColumnName("og_max");

                entity.Property(e => e.OgMin).HasColumnName("og_min");

                entity.Property(e => e.Profile)
                    .HasColumnName("profile")
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.StyleGuide)
                    .HasColumnName("style_guide")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("supplier");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.ContactEmail)
                    .HasColumnName("contact_email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactFirstName)
                    .HasColumnName("contact_first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactLastName)
                    .HasColumnName("contact_last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhone)
                    .HasColumnName("contact_phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplierAddress>(entity =>
            {
                entity.HasKey(e => new { e.SupplierId, e.AddressId, e.AddressTypeId })
                    .HasName("PRIMARY");

                entity.ToTable("supplier_address");

                entity.HasIndex(e => e.AddressId)
                    .HasName("supplier_address_address_FK_idx");

                entity.HasIndex(e => e.AddressTypeId)
                    .HasName("supplier_address_address_type_FK_idx");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.AddressTypeId).HasColumnName("address_type_id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.SupplierAddress)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supplier_address_address_FK");

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.SupplierAddress)
                    .HasForeignKey(d => d.AddressTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supplier_address_address_type_FK");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierAddress)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supplier_address_supplier_FK");
            });

            modelBuilder.Entity<UnitType>(entity =>
            {
                entity.ToTable("unit_type");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UnitTypeId).HasColumnName("unit_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UseDuring>(entity =>
            {
                entity.ToTable("use_during");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UseDuringId).HasColumnName("use_during_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Yeast>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("PRIMARY");

                entity.ToTable("yeast");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.AddToSecondary).HasColumnName("add_to_secondary");

                entity.Property(e => e.Attenuation).HasColumnName("attenuation");

                entity.Property(e => e.BestFor)
                    .HasColumnName("best_for")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Flocculation)
                    .HasColumnName("flocculation")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Form)
                    .HasColumnName("form")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Laboratory)
                    .HasColumnName("laboratory")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaxReuse).HasColumnName("max_reuse");

                entity.Property(e => e.MaxTemp).HasColumnName("max_temp");

                entity.Property(e => e.MinTemp).HasColumnName("min_temp");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Ingredient)
                    .WithOne(p => p.Yeast)
                    .HasForeignKey<Yeast>(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("yeast_ingredient_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
