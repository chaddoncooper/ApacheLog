import { async, TestBed } from '@angular/core/testing';
import { VirtualHostsModule } from './virtual-hosts.module';

describe('VirtualHostsModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [VirtualHostsModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(VirtualHostsModule).toBeDefined();
  });
});
