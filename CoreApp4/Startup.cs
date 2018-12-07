
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YllariFM.Models.DB;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using YllariFM.Source.ViewModels.Api;
using YllariFM.Source.ViewModels.Vistas;
using YllariFM.Source.ViewModels;

namespace CoreApp4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            configurarMapper();
        }

        public void configurarMapper() {
            Mapper.Initialize(cfg =>
            {

                //api
                cfg.CreateMap<Agencia, AgenciaDto>().ReverseMap();
                cfg.CreateMap<ActualizarAgenciaDto, Agencia>().ReverseMap();
                cfg.CreateMap<Biblia, YllariFM.Source.ViewModels.BibliasVms.BibliaDto>().ReverseMap();

                //vms de vistas
                cfg.CreateMap<ListarAgenciaVm, Agencia>().ReverseMap();
                //cfg.CreateMap<BibliasVms.LeerIndexBibliaDto, Biblia>().ReverseMap();
            });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<YllariFmContext>(options => options.UseSqlServer(Configuration.GetConnectionString("YllariFmDb")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
