describe('My First Test', () => {
    it('clicks the link "Locatie"', () => {
        cy.visit('http://localhost:8081');

        cy.get('.input-container').first()
            .type('a@a.nl')
            .next().type('a')
        cy.contains('Inloggen').click()

        cy.reload();

        cy.contains('Locaties').click();

        cy.url().should('include', '/locaties');

        cy.get('#app > div > div.component-container.no-padding > div > div:nth-child(2) > svg').click();

        cy.contains('Locatie toevoegen');
        // cy.get('.search-fields').find('.svg-inline--fa fa-plus-square fa-w-14 plus-location')
        // cy.get('<path>')
        // cy.get('<svg>')
        // cy.get('<path class="" ' +
        //     'fill="currentColor" ' +
        //     'd="M400 32H48C21.5 32 0 53.5 0 80v352c0 26.5 21.5 48 48 48h352c26.5 0 48-21.5 48-48V80c0-26.5-21.5-48-48-48zm-32 252c0 6.6-5.4 12-12 12h-92v92c0 6.6-5.4 12-12 12h-56c-6.6 0-12-5.4-12-12v-92H92c-6.6 0-12-5.4-12-12v-56c0-6.6 5.4-12 12-12h92v-92c0-6.6 5.4-12 12-12h56c6.6 0 12 5.4 12 12v92h92c6.6 0 12 5.4 12 12v56z">' +
        //     '</path>').click()

        cy.get('.custom-select').click()

        cy.contains('Stad').click()

        //cy.request('https://localhost:44311/api/locations/rooms')
    })
});
