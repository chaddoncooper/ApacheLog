import { TestBed } from '@angular/core/testing';

import { VirtualHostsService } from './virtual-hosts.service';

describe('VirtualHostsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VirtualHostsService = TestBed.get(VirtualHostsService);
    expect(service).toBeTruthy();
  });
});
