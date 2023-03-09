describe('Find package id', () => {
    it('opens package overview', () => {
        cy.visit('http://localhost:8081');

        cy.get('.input-container').first()
            .type('a@a.nl')
            .next().type('a')
        cy.contains('Inloggen').click()

        cy.reload();
        cy.contains('Zoeken').click();
        cy.contains('Dikke stift').click();
        cy.get('.qr-subheader').then(($el) =>{
            var text = $el.text()
            expect(text).to.eq("Pakket ID: 192870fb-2839-4e05-878d-08d97e83b9bd");
            var guid = text.split(': ').pop();
            expect(guid).to.eq("192870fb-2839-4e05-878d-08d97e83b9bd");

        })
    })
});
