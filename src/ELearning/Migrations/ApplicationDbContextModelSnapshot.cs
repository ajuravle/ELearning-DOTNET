using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ELearning.Data;

namespace ELearning.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ELearning.Model.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerText")
                        .IsRequired();

                    b.Property<bool>("Correct");

                    b.Property<Guid>("QuestionId");

                    b.HasKey("Id");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("ELearning.Model.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdTopic");

                    b.Property<string>("QuestionText")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ELearning.Model.Technology", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdProfessor");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("UrlImage")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("ELearning.Model.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdTechnology");

                    b.Property<string>("TopicName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("ELearning.Model.UniversityUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Firstname")
                        .IsRequired();

                    b.Property<string>("Lastname")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<string>("email")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("UniversityUsers");
                });
        }
    }
}
