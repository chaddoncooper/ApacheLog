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
    path: 'resources',
    loadChildren: () =>
      import('@apache-log/resources').then(m => m.ResourcesModule)
  },
  {
    path: 'virtual-hosts',
    loadChildren: () =>
      import('@apache-log/virtual-hosts').then(m => m.VirtualHostsModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
