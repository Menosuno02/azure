using Microsoft.AspNetCore.Mvc;
using MvcCoreApiClient.Models;
using MvcCoreApiClient.Services;

namespace MvcCoreApiClient.Controllers
{
    public class DoctoresController : Controller
    {
        private ServiceDoctores service;

        public DoctoresController(ServiceDoctores service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Doctor> doctores =
                await this.service.GetDoctoresAsync();
            return View(doctores);
        }

        public async Task<IActionResult> Details(int id)
        {
            Doctor doctor = await this.service.FindDoctorAsync(id);
            return View(doctor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteDoctorAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            await this.service.InsertDoctorAsync(doctor.HospitalCod,
                doctor.DoctorNo, doctor.Apellido, doctor.Especialidad,
                doctor.Salario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Doctor doctor = await this.service.FindDoctorAsync(id);
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Doctor doctor)
        {
            await this.service.UpdateDoctorAsync
                (doctor.HospitalCod, doctor.DoctorNo, doctor.Apellido,
                doctor.Especialidad, doctor.Salario);
            return RedirectToAction("Index");
        }
    }
}
