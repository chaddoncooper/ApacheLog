import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageVirtualHostsComponent } from './manage-virtual-hosts.component';

describe('ManageVirtualHostsComponent', () => {
  let component: ManageVirtualHostsComponent;
  let fixture: ComponentFixture<ManageVirtualHostsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageVirtualHostsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageVirtualHostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
