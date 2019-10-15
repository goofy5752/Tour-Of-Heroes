import { UnauthorizedViewComponent } from './layout-view/unauthorized-view/unauthorized-view.component';
import { HeroesMainContentComponent } from './main-content/heroes-main-content/heroes-main-content.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeroDetailComponent } from './content/hero-detail/hero-detail.component';
import { AddHeroMainComponent } from './main-content/add-hero-main/add-hero-main.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
  {path: '', redirectTo: 'heroes', pathMatch: 'full'},
  { path: 'detail/:id', component: HeroDetailComponent, canActivate: [AuthGuard] },
  { path: 'heroes', component: HeroesMainContentComponent, canActivate: [AuthGuard] },
  { path: 'add-hero', component: AddHeroMainComponent, canActivate: [AuthGuard] },
  {
    path: 'user', component: UnauthorizedViewComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
