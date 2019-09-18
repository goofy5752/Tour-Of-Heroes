import { HeroesMainContentComponent } from './heroes-main-content/heroes-main-content.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeroDetailComponent } from './hero-detail/hero-detail.component';
import { AddHeroComponent } from './add-hero/add-hero.component';

const routes: Routes = [
  { path: '', redirectTo: '/heroes', pathMatch: 'full' },
  { path: 'detail/:id', component: HeroDetailComponent },
  { path: 'heroes', component: HeroesMainContentComponent },
  { path: 'add-hero', component: AddHeroComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
