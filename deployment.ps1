$appname = 'RUCSpecialeFunctionProject'
$rgname = "rg-$appname"
$prodsvcplanname = "afp-$appname-prod-001"
# Storage account names needs to be globally unique and below 24 chars
$saname = "rucspecialesa2021" 
$location = 'West Europe'

# Create resource group
New-AzResourceGroup -Name $rgname -Location $location

# Storageaccount to store state
New-AzStorageAccount -ResourceGroupName $rgname `
    -AccountName $saName `
    -Location $location `
    -SkuName Standard_LRS `
    -AccessTier Hot

# Premium App Service Plan for the production app
New-AzFunctionAppPlan -ResourceGroupName $rgname `
    -Name $prodsvcplanname `
    -Location $location `
    -MinimumWorkerCount 1 `
    -MaximumWorkerCount 10 `
    -Sku EP1 `
    -WorkerType Windows

# Function App for Production
New-AzFunctionApp -ResourceGroupName $rgname `
    -Name "afp-$appname-prod-001" `
    -Location $location `
    -StorageAccountName $saName `
    -PlanName $prodsvcplanname
    -OSType Windows `
    -Runtime DotNet `
    -RuntimeVersion 3 `
    -FunctionsVersion 3

# The dev/test function app 
# Since I don't supply a Plan Name it will create 
# a serverless consumption plan for me
New-AzFunctionApp -ResourceGroupName $rgname `
    -Name "afa-$appname-dev-001" `
    -Location $location `
    -StorageAccountName $saName `
    -OSType Windows `
    -Runtime DotNet `
    -RuntimeVersion 3 `
    -FunctionsVersion 3