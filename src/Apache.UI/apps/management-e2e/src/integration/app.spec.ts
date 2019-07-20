import { getGreeting } from '../support/app.po';

describe('management', () => {
  beforeEach(() => cy.visit('/'));

  it('should display welcome message', () => {
    getGreeting().contains('Welcome to management!');
  });
});
