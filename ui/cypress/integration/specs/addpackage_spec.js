const { faCarSide } = require("@fortawesome/free-solid-svg-icons");

describe('Test if the user can add a package', () => {
    it('Tests the registreren function', () => {

        cy.visit('http://localhost:8081');

        cy.get('.input-container').first()
            .type('a@a.nl')
            .next().type('a')
        cy.contains('Inloggen').click()

        cy.reload();

        cy.contains('Registreren').click();

        cy.get('.input').first().type("Otto Eduard Leopold von Bismarck-Schönhausen");
        cy.get('.input').eq(1).type('Paarse eenwieler');
        cy.get('.input').eq(2).click().then(option => {cy.wrap(option).siblings().contains("Otto Eduard Leopold von Bismarck-Schönhausen").click()});
        cy.get('.input').eq(3).click().then(option => {cy.wrap(option).siblings().contains("Tillywood, Billy's osso, B6.6").click()});
        cy.get('.input').eq(4).click().then(option => {cy.wrap(option).siblings().contains("Otto Eduard Leopold von Bismarck-Schönhausen").click()});

        cy.contains('Volgende').click();
        cy.contains('Bevestigen').click();


        cy.get('.logo').click();

        cy.get('#app > div.nav-container > div > div.logo-container > div').click();

        cy.contains('Zoeken').click();

        cy.contains("Paarse eenwieler");
    })
  })