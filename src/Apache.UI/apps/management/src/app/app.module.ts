import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BlacklistModule } from '@apache/blacklist';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, BlacklistModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
