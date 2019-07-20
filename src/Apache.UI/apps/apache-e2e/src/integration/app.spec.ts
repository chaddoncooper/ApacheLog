import { getGreeting } from '../support/app.po';

describe('apache', () => {
  beforeEach(() => cy.visit('/'));

  it('should display welcome message', () => {
    getGreeting().contains('Welcome to apache!');
  });
});
