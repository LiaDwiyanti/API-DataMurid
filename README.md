# API Data Murid
API Data Murid adalah proyek Web API sederhana berbasis ASP.NET Core untuk mengelola data murid. API ini terdapat fitur CRUD dan autentikasi user untuk mengakses fitur CRUD tersebut. Dibuat sebagai bagian dari portofolio backend developer, proyek ini belum dideploy dan hanya berjalan secara lokal.

## Endpoint
### Autentikasi
- POST /register
- POST /login

### CRUD
- GET /api/murid
- POST /api/murid/add
- PUT /api/murid/update/{muridID}
- DELETE /api/murid/delete/{muridID}
