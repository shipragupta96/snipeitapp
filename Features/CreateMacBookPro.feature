Feature: Asset creation in Snipe‑IT demo

  Background:
    Given I am logged in as admin on the Snipe‑IT demo

  Scenario: Create and verify a MacBook Pro 13" asset
    When I create a 'MacBook Pro 13"' asset with status "Ready to Deploy" checked out to a random user
    Then I should see the asset in the asset list
    When I navigate to the asset details page
    Then the asset details 'Macbook Pro 13"' with status "Ready to Deploy" is created successfully
    And the "history" tab shows the checkout event properly