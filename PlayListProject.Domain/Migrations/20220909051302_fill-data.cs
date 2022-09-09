using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayListProject.Domain.Migrations
{
    public partial class filldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Authors (Id, Name)
                                VALUES 
                                (1, 'Iron Maiden'),
                                (2, 'Metallica'),
                                (3, 'Oasis'),
                                (4, 'Andres Cepeda'),
                                (5, 'Coldplay'),
                                (6, 'Grupo Niche');");
            migrationBuilder.Sql(@"INSERT INTO Songs (Id, Name, AuthorId)
                                VALUES 
                                (1,'Run to the hills',1),
                                (2,'Fear of the dark',1),
                                (3,'Stratego',1),
                                (4,'Whisky in the jar',2),
                                (5,'Wherever I may roam',2),
                                (6,'Seek and destroy',2),
                                (7,'Wonderwall',3),
                                (8,'Besos usados',4),
                                (9,'Yellow',5),
                                (10,'Paradise',5),
                                (11,'Magic',5),
                                (12,'Al pasito',6);");
            migrationBuilder.Sql(@"INSERT INTO PlayLists (Id, Name, Description)
                                VALUES 
                                (1,'Metal', 'Metal'),
                                (2,'Pop Rock', 'Pop rock'),
                                (3,'Otros', 'Pop rock');");
            migrationBuilder.Sql(@"INSERT INTO PlayListSong (PlayListId, SongId)
                                VALUES 
                                (1,1),
                                (1,2),
                                (1,3),
                                (1,4),
                                (1,5),
                                (1,6),
                                (2,7),
                                (2,9),
                                (2,10),
                                (2,11),
                                (3,8),
                                (3,12);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
