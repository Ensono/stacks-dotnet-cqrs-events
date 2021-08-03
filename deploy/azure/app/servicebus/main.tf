data "azurerm_client_config" "current" {}

# Naming convention
module "default_label" {
  source     = "git::https://github.com/cloudposse/terraform-null-label.git?ref=0.24.1"
  namespace  = "${var.name_company}-${var.name_project}"
  stage      = var.stage
  name       = "${lookup(var.location_name_map, var.resource_group_location, "westeurope")}-${var.name_domain}"
  attributes = var.attributes
  delimiter  = "-"
  tags       = var.tags
}

# create a random string to be used to create
# similar but different resources
resource "random_string" "seed" {
  length  = var.seed_length
  upper   = false
  number  = false
  special = false
}
