import { InjectionToken } from '@angular/core';
import { Environment } from '@apache-log/models';

export const APACHE_LOG_ENV = new InjectionToken<Environment>(
  'Apache Log Environment'
);
