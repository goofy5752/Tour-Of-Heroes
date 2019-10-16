import { UserComponent } from './user/user.component';
import { HeroesMainContentComponent } from './main-content/heroes-main-content/heroes-main-content.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeroDetailComponent } from './content/hero-detail/hero-detail.component';
import { AddHeroMainComponent } from './main-content/add-hero-main/add-hero-main.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: 'heroes', pathMatch: 'full' },
  { path: 'detail/:id', component: HeroDetailComponent, canActivate: [AuthGuard], data: {title: 'Details'} },
  { path: 'heroes', component: HeroesMainContentComponent, canActivate: [AuthGuard], data: {title: 'Heroes'} },
  { path: 'add-hero', component: AddHeroMainComponent, canActivate: [AuthGuard], data: {title: 'Add hero'} },
  {
    path: 'user', component: UserComponent, data: {title: 'Authenticate'},
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
