using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using MovieReviewer.Models;

namespace MovieReviewer.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.HasMany(user => user.PreferredMovies)
                .WithMany(Movie => Movie.UsersPreferred);

            user.HasMany(user => user.LikedMovies)
                .WithMany(Movie => Movie.LikedByUsers);

            user.HasMany(user => user.DislikedMovies)
                .WithMany(Movie => Movie.DislikedByUsers);
        }
    }
}
