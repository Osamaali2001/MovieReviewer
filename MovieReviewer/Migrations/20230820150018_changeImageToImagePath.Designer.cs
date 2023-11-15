﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieReviewer.Data;

#nullable disable

namespace MovieReviewer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230820150018_changeImageToImagePath")]
    partial class changeImageToImagePath
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.Property<short>("ActtorsInId")
                        .HasColumnType("smallint");

                    b.Property<short>("MoviesInMovieId")
                        .HasColumnType("smallint");

                    b.HasKey("ActtorsInId", "MoviesInMovieId");

                    b.HasIndex("MoviesInMovieId");

                    b.ToTable("ActorMovie");
                });

            modelBuilder.Entity("ActorUser", b =>
                {
                    b.Property<short>("PreferredActorsId")
                        .HasColumnType("smallint");

                    b.Property<short>("UsersPreferredId")
                        .HasColumnType("smallint");

                    b.HasKey("PreferredActorsId", "UsersPreferredId");

                    b.HasIndex("UsersPreferredId");

                    b.ToTable("ActorUser");
                });

            modelBuilder.Entity("DirectorUser", b =>
                {
                    b.Property<short>("PreferredDirectorsId")
                        .HasColumnType("smallint");

                    b.Property<short>("UsersPreferredId")
                        .HasColumnType("smallint");

                    b.HasKey("PreferredDirectorsId", "UsersPreferredId");

                    b.HasIndex("UsersPreferredId");

                    b.ToTable("DirectorUser");
                });

            modelBuilder.Entity("MovieReviewer.Models.Actor", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<byte>("Age")
                        .HasColumnType("tinyint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("MovieReviewer.Models.Director", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<byte>("Age")
                        .HasColumnType("tinyint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Director");
                });

            modelBuilder.Entity("MovieReviewer.Models.Movie", b =>
                {
                    b.Property<short>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("MovieId"));

                    b.Property<short>("DirectorId")
                        .HasColumnType("smallint");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MovieName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MovieId");

                    b.HasIndex("DirectorId");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("MovieReviewer.Models.User", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MovieUser", b =>
                {
                    b.Property<short>("PreferredMoviesMovieId")
                        .HasColumnType("smallint");

                    b.Property<short>("UsersPreferredId")
                        .HasColumnType("smallint");

                    b.HasKey("PreferredMoviesMovieId", "UsersPreferredId");

                    b.HasIndex("UsersPreferredId");

                    b.ToTable("MovieUser");
                });

            modelBuilder.Entity("MovieUser1", b =>
                {
                    b.Property<short>("LikedByUsersId")
                        .HasColumnType("smallint");

                    b.Property<short>("LikedMoviesMovieId")
                        .HasColumnType("smallint");

                    b.HasKey("LikedByUsersId", "LikedMoviesMovieId");

                    b.HasIndex("LikedMoviesMovieId");

                    b.ToTable("MovieUser1");
                });

            modelBuilder.Entity("MovieUser2", b =>
                {
                    b.Property<short>("DislikedByUsersId")
                        .HasColumnType("smallint");

                    b.Property<short>("DislikedMoviesMovieId")
                        .HasColumnType("smallint");

                    b.HasKey("DislikedByUsersId", "DislikedMoviesMovieId");

                    b.HasIndex("DislikedMoviesMovieId");

                    b.ToTable("MovieUser2");
                });

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.HasOne("MovieReviewer.Models.Actor", null)
                        .WithMany()
                        .HasForeignKey("ActtorsInId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieReviewer.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesInMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ActorUser", b =>
                {
                    b.HasOne("MovieReviewer.Models.Actor", null)
                        .WithMany()
                        .HasForeignKey("PreferredActorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieReviewer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersPreferredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DirectorUser", b =>
                {
                    b.HasOne("MovieReviewer.Models.Director", null)
                        .WithMany()
                        .HasForeignKey("PreferredDirectorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieReviewer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersPreferredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieReviewer.Models.Movie", b =>
                {
                    b.HasOne("MovieReviewer.Models.Director", "Director")
                        .WithMany("MoviesDirected")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");
                });

            modelBuilder.Entity("MovieUser", b =>
                {
                    b.HasOne("MovieReviewer.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("PreferredMoviesMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieReviewer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersPreferredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieUser1", b =>
                {
                    b.HasOne("MovieReviewer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("LikedByUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieReviewer.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("LikedMoviesMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieUser2", b =>
                {
                    b.HasOne("MovieReviewer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("DislikedByUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieReviewer.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("DislikedMoviesMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieReviewer.Models.Director", b =>
                {
                    b.Navigation("MoviesDirected");
                });
#pragma warning restore 612, 618
        }
    }
}
