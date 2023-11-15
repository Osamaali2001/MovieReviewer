using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewer.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables_UserMovieActorDirector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Age = table.Column<byte>(type: "tinyint", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Director",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Age = table.Column<byte>(type: "tinyint", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Director", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    MovieId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DirectorId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movie_Director_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Director",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorUser",
                columns: table => new
                {
                    PreferredActorsId = table.Column<short>(type: "smallint", nullable: false),
                    UsersPreferredId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorUser", x => new { x.PreferredActorsId, x.UsersPreferredId });
                    table.ForeignKey(
                        name: "FK_ActorUser_Actor_PreferredActorsId",
                        column: x => x.PreferredActorsId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorUser_User_UsersPreferredId",
                        column: x => x.UsersPreferredId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectorUser",
                columns: table => new
                {
                    PreferredDirectorsId = table.Column<short>(type: "smallint", nullable: false),
                    UsersPreferredId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorUser", x => new { x.PreferredDirectorsId, x.UsersPreferredId });
                    table.ForeignKey(
                        name: "FK_DirectorUser_Director_PreferredDirectorsId",
                        column: x => x.PreferredDirectorsId,
                        principalTable: "Director",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectorUser_User_UsersPreferredId",
                        column: x => x.UsersPreferredId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    ActtorsInId = table.Column<short>(type: "smallint", nullable: false),
                    MoviesInMovieId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.ActtorsInId, x.MoviesInMovieId });
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actor_ActtorsInId",
                        column: x => x.ActtorsInId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movie_MoviesInMovieId",
                        column: x => x.MoviesInMovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieUser",
                columns: table => new
                {
                    PreferredMoviesMovieId = table.Column<short>(type: "smallint", nullable: false),
                    UsersPreferredId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieUser", x => new { x.PreferredMoviesMovieId, x.UsersPreferredId });
                    table.ForeignKey(
                        name: "FK_MovieUser_Movie_PreferredMoviesMovieId",
                        column: x => x.PreferredMoviesMovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieUser_User_UsersPreferredId",
                        column: x => x.UsersPreferredId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieUser1",
                columns: table => new
                {
                    LikedByUsersId = table.Column<short>(type: "smallint", nullable: false),
                    LikedMoviesMovieId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieUser1", x => new { x.LikedByUsersId, x.LikedMoviesMovieId });
                    table.ForeignKey(
                        name: "FK_MovieUser1_Movie_LikedMoviesMovieId",
                        column: x => x.LikedMoviesMovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieUser1_User_LikedByUsersId",
                        column: x => x.LikedByUsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieUser2",
                columns: table => new
                {
                    DislikedByUsersId = table.Column<short>(type: "smallint", nullable: false),
                    DislikedMoviesMovieId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieUser2", x => new { x.DislikedByUsersId, x.DislikedMoviesMovieId });
                    table.ForeignKey(
                        name: "FK_MovieUser2_Movie_DislikedMoviesMovieId",
                        column: x => x.DislikedMoviesMovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieUser2_User_DislikedByUsersId",
                        column: x => x.DislikedByUsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MoviesInMovieId",
                table: "ActorMovie",
                column: "MoviesInMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_ActorUser_UsersPreferredId",
                table: "ActorUser",
                column: "UsersPreferredId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectorUser_UsersPreferredId",
                table: "DirectorUser",
                column: "UsersPreferredId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_DirectorId",
                table: "Movie",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieUser_UsersPreferredId",
                table: "MovieUser",
                column: "UsersPreferredId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieUser1_LikedMoviesMovieId",
                table: "MovieUser1",
                column: "LikedMoviesMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieUser2_DislikedMoviesMovieId",
                table: "MovieUser2",
                column: "DislikedMoviesMovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.DropTable(
                name: "ActorUser");

            migrationBuilder.DropTable(
                name: "DirectorUser");

            migrationBuilder.DropTable(
                name: "MovieUser");

            migrationBuilder.DropTable(
                name: "MovieUser1");

            migrationBuilder.DropTable(
                name: "MovieUser2");

            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Director");
        }
    }
}
