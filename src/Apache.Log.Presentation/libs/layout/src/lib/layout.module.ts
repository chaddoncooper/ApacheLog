import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainLayoutComponent } from './containers/main-layout/main-layout.component';
import { IconsProviderModule } from './icons-provider.module';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [CommonModule, IconsProviderModule, NgZorroAntdModule, RouterModule],
  declarations: [MainLayoutComponent],
  exports: [MainLayoutComponent]
})
export class LayoutModule {}
