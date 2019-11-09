import { UserDetailComponent } from './admin/user-detail/user-detail.component';
import { UserControllerComponent } from './admin/user-controller/user-controller.component';
import { ForbiddenComponent } from './user/forbidden/forbidden.component';
import { AddBlogComponent } from './admin/add-blog/add-blog.component';
import { AdminComponent } from './admin/admin.component';
import { BlogDetailComponent } from './content/blog-detail/blog-detail.component';
import { UserComponent } from './user/user.component';
import { HeroesMainContentComponent } from './main-content/heroes-main-content/heroes-main-content.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeroDetailComponent } from './content/hero-detail/hero-detail.component';
import { AddHeroMainComponent } from './main-content/add-hero-main/add-hero-main.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { AuthGuard } from './auth/auth.guard';
import { ProfileComponent } from './user/profile/profile.component';
import { BlogComponent } from './content/blog/blog.component';

const routes: Routes = [
  { path: '', redirectTo: 'heroes', pathMatch: 'full' },
  { path: 'detail/:id', component: HeroDetailComponent, canActivate: [AuthGuard] },
  { path: 'heroes', component: HeroesMainContentComponent, canActivate: [AuthGuard] },
  { path: 'blog', component: BlogComponent, canActivate: [AuthGuard] },
  { path: 'blog/:id', component: BlogDetailComponent, canActivate: [AuthGuard] },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ]
  },
  { path: 'user/profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'user/forbidden', component: ForbiddenComponent },
  { path: 'admin', component: AdminComponent, canActivate: [AuthGuard], data: { permittedRoles: ['Admin'] } },
  { path: 'admin/user-controller', component: UserControllerComponent, canActivate: [AuthGuard], data: { permittedRoles: ['Admin'] } },
  { path: 'admin/add-hero', component: AddHeroMainComponent, canActivate: [AuthGuard], data: { permittedRoles: ['Admin'] } },
  { path: 'admin/add-blog', component: AddBlogComponent, canActivate: [AuthGuard], data: { permittedRoles: ['Admin', 'Editor'] } },
  { path: 'user-detail/:id', component: UserDetailComponent, canActivate: [AuthGuard], data: { permittedRoles: ['Admin'] } }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
