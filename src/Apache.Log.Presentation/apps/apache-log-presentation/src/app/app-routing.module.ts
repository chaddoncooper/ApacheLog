import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  // { path: '', pathMatch: 'full', redirectTo: '/overview' },
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
