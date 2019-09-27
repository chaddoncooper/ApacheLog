import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/dashboard/overview' },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('@apache-log/dashboard').then(m => m.DashboardModule)
  },
  {
    path: 'blacklist',
    loadChildren: () =>
      import('@apache-log/blacklist').then(m => m.BlacklistModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
