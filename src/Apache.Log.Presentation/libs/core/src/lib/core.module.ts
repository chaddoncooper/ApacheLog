import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Environment } from '@apache-log/models';
import { APACHE_LOG_ENV } from './tokens/apache-log-environment.token';

@NgModule({
  imports: [CommonModule]
})
export class CoreModule {
  static forRoot(environment: Environment): ModuleWithProviders<CoreModule> {
    return {
      ngModule: CoreModule,
      providers: [{ provide: APACHE_LOG_ENV, useValue: environment }]
    };
  }
}
