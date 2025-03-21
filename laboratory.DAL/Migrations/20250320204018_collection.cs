using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace laboratory.DAL.Migrations
{
    /// <inheritdoc />
    public partial class collection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentChemicals_Chemicals_ChemicalsId",
                table: "ExperimentChemicals");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentChemicals_Experiments_ExperimentsId",
                table: "ExperimentChemicals");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentEquipments_Equipments_EquipmentsId",
                table: "ExperimentEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentEquipments_Experiments_ExperimentsId",
                table: "ExperimentEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExperimentEquipments",
                table: "ExperimentEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExperimentChemicals",
                table: "ExperimentChemicals");

            migrationBuilder.RenameTable(
                name: "ExperimentEquipments",
                newName: "EquipmentExperiment");

            migrationBuilder.RenameTable(
                name: "ExperimentChemicals",
                newName: "ChemicalExperiment");

            migrationBuilder.RenameIndex(
                name: "IX_ExperimentEquipments_ExperimentsId",
                table: "EquipmentExperiment",
                newName: "IX_EquipmentExperiment_ExperimentsId");

            migrationBuilder.RenameIndex(
                name: "IX_ExperimentChemicals_ExperimentsId",
                table: "ChemicalExperiment",
                newName: "IX_ChemicalExperiment_ExperimentsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentExperiment",
                table: "EquipmentExperiment",
                columns: new[] { "EquipmentsId", "ExperimentsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChemicalExperiment",
                table: "ChemicalExperiment",
                columns: new[] { "ChemicalsId", "ExperimentsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChemicalExperiment_Chemicals_ChemicalsId",
                table: "ChemicalExperiment",
                column: "ChemicalsId",
                principalTable: "Chemicals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChemicalExperiment_Experiments_ExperimentsId",
                table: "ChemicalExperiment",
                column: "ExperimentsId",
                principalTable: "Experiments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentExperiment_Equipments_EquipmentsId",
                table: "EquipmentExperiment",
                column: "EquipmentsId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentExperiment_Experiments_ExperimentsId",
                table: "EquipmentExperiment",
                column: "ExperimentsId",
                principalTable: "Experiments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChemicalExperiment_Chemicals_ChemicalsId",
                table: "ChemicalExperiment");

            migrationBuilder.DropForeignKey(
                name: "FK_ChemicalExperiment_Experiments_ExperimentsId",
                table: "ChemicalExperiment");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentExperiment_Equipments_EquipmentsId",
                table: "EquipmentExperiment");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentExperiment_Experiments_ExperimentsId",
                table: "EquipmentExperiment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentExperiment",
                table: "EquipmentExperiment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChemicalExperiment",
                table: "ChemicalExperiment");

            migrationBuilder.RenameTable(
                name: "EquipmentExperiment",
                newName: "ExperimentEquipments");

            migrationBuilder.RenameTable(
                name: "ChemicalExperiment",
                newName: "ExperimentChemicals");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentExperiment_ExperimentsId",
                table: "ExperimentEquipments",
                newName: "IX_ExperimentEquipments_ExperimentsId");

            migrationBuilder.RenameIndex(
                name: "IX_ChemicalExperiment_ExperimentsId",
                table: "ExperimentChemicals",
                newName: "IX_ExperimentChemicals_ExperimentsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExperimentEquipments",
                table: "ExperimentEquipments",
                columns: new[] { "EquipmentsId", "ExperimentsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExperimentChemicals",
                table: "ExperimentChemicals",
                columns: new[] { "ChemicalsId", "ExperimentsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentChemicals_Chemicals_ChemicalsId",
                table: "ExperimentChemicals",
                column: "ChemicalsId",
                principalTable: "Chemicals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentChemicals_Experiments_ExperimentsId",
                table: "ExperimentChemicals",
                column: "ExperimentsId",
                principalTable: "Experiments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentEquipments_Equipments_EquipmentsId",
                table: "ExperimentEquipments",
                column: "EquipmentsId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentEquipments_Experiments_ExperimentsId",
                table: "ExperimentEquipments",
                column: "ExperimentsId",
                principalTable: "Experiments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
