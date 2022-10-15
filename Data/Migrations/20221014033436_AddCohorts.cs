using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRIDGEWebApp.Data.Migrations
{
    public partial class AddCohorts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cohort_AspNetUsers_CreatedBy",
                table: "Cohort");

            migrationBuilder.DropForeignKey(
                name: "FK_Cohort_AspNetUsers_UpdatedBy",
                table: "Cohort");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Cohort_CohortId",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cohort",
                table: "Cohort");

            migrationBuilder.RenameTable(
                name: "Cohort",
                newName: "Cohorts");

            migrationBuilder.RenameIndex(
                name: "IX_Cohort_UpdatedBy",
                table: "Cohorts",
                newName: "IX_Cohorts_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Cohort_CreatedBy",
                table: "Cohorts",
                newName: "IX_Cohorts_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cohorts",
                table: "Cohorts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cohorts_AspNetUsers_CreatedBy",
                table: "Cohorts",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cohorts_AspNetUsers_UpdatedBy",
                table: "Cohorts",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Cohorts_CohortId",
                table: "Participants",
                column: "CohortId",
                principalTable: "Cohorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cohorts_AspNetUsers_CreatedBy",
                table: "Cohorts");

            migrationBuilder.DropForeignKey(
                name: "FK_Cohorts_AspNetUsers_UpdatedBy",
                table: "Cohorts");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Cohorts_CohortId",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cohorts",
                table: "Cohorts");

            migrationBuilder.RenameTable(
                name: "Cohorts",
                newName: "Cohort");

            migrationBuilder.RenameIndex(
                name: "IX_Cohorts_UpdatedBy",
                table: "Cohort",
                newName: "IX_Cohort_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Cohorts_CreatedBy",
                table: "Cohort",
                newName: "IX_Cohort_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cohort",
                table: "Cohort",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cohort_AspNetUsers_CreatedBy",
                table: "Cohort",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cohort_AspNetUsers_UpdatedBy",
                table: "Cohort",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Cohort_CohortId",
                table: "Participants",
                column: "CohortId",
                principalTable: "Cohort",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
