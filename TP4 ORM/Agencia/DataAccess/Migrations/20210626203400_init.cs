using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    esAdmin = table.Column<bool>(type: "bit", nullable: false),
                    bloqueado = table.Column<bool>(type: "bit", nullable: false),
                    intentosLogueo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cabania",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    barrio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estrellas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cantidadDePersonas = table.Column<int>(type: "int", nullable: false),
                    tv = table.Column<bool>(type: "bit", nullable: false),
                    ciudadid = table.Column<int>(type: "int", nullable: true),
                    cantidadDeBanios = table.Column<int>(type: "int", nullable: false),
                    precio_por_dia = table.Column<double>(type: "float", nullable: false),
                    habitaciones = table.Column<double>(type: "float", nullable: false),
                    reservado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabania", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cabania_Ciudades_ciudadid",
                        column: x => x.ciudadid,
                        principalTable: "Ciudades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    barrio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estrellas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cantidadDePersonas = table.Column<int>(type: "int", nullable: false),
                    tv = table.Column<bool>(type: "bit", nullable: false),
                    ciudadid = table.Column<int>(type: "int", nullable: true),
                    precio_por_persona = table.Column<double>(type: "float", nullable: false),
                    reservado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.id);
                    table.ForeignKey(
                        name: "FK_Hotel_Ciudades_ciudadid",
                        column: x => x.ciudadid,
                        principalTable: "Ciudades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hotelid = table.Column<int>(type: "int", nullable: true),
                    cabaniaid = table.Column<int>(type: "int", nullable: true),
                    cantAlojamientos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_Agencia_Cabania_cabaniaid",
                        column: x => x.cabaniaid,
                        principalTable: "Cabania",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agencia_Hotel_hotelid",
                        column: x => x.hotelid,
                        principalTable: "Hotel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FDesde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FHasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioid = table.Column<int>(type: "int", nullable: true),
                    precio = table.Column<float>(type: "real", nullable: false),
                    cabaniaid = table.Column<int>(type: "int", nullable: true),
                    hotelid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reserva_Cabania_cabaniaid",
                        column: x => x.cabaniaid,
                        principalTable: "Cabania",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reserva_Hotel_hotelid",
                        column: x => x.hotelid,
                        principalTable: "Hotel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reserva_Usuario_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgenciaManager",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuarioid = table.Column<int>(type: "int", nullable: true),
                    reservaid = table.Column<int>(type: "int", nullable: true),
                    agenciaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgenciaManager", x => x.id);
                    table.ForeignKey(
                        name: "FK_AgenciaManager_Agencia_agenciaid",
                        column: x => x.agenciaid,
                        principalTable: "Agencia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgenciaManager_Reserva_reservaid",
                        column: x => x.reservaid,
                        principalTable: "Reserva",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgenciaManager_Usuario_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Ciudades",
                columns: new[] { "id", "nombre" },
                values: new object[,]
                {
                    { 1, "buenos aires" },
                    { 2, "rio negro" },
                    { 3, "la plata" },
                    { 4, "bariloche" },
                    { 5, "lujan" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "id", "DNI", "bloqueado", "esAdmin", "intentosLogueo", "mail", "nombre", "pass" },
                values: new object[,]
                {
                    { 1, 101010, false, true, 0, "soporte@gmail.com", "admin", "123" },
                    { 2, 54594166, false, false, 0, "soporte@gmail.com", "gianpool", "123" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agencia_cabaniaid",
                table: "Agencia",
                column: "cabaniaid");

            migrationBuilder.CreateIndex(
                name: "IX_Agencia_hotelid",
                table: "Agencia",
                column: "hotelid");

            migrationBuilder.CreateIndex(
                name: "IX_AgenciaManager_agenciaid",
                table: "AgenciaManager",
                column: "agenciaid");

            migrationBuilder.CreateIndex(
                name: "IX_AgenciaManager_reservaid",
                table: "AgenciaManager",
                column: "reservaid");

            migrationBuilder.CreateIndex(
                name: "IX_AgenciaManager_usuarioid",
                table: "AgenciaManager",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Cabania_ciudadid",
                table: "Cabania",
                column: "ciudadid");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_ciudadid",
                table: "Hotel",
                column: "ciudadid");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_cabaniaid",
                table: "Reserva",
                column: "cabaniaid");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_hotelid",
                table: "Reserva",
                column: "hotelid");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_usuarioid",
                table: "Reserva",
                column: "usuarioid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgenciaManager");

            migrationBuilder.DropTable(
                name: "Agencia");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Cabania");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Ciudades");
        }
    }
}
