#include "accountsdata.h"
#include "qdebug.h"


#include "CTaxcode.h"

AccountsData::AccountsData()
{

}

bool AccountsData::taxcode_can_add(const CTaxcode taxcode) const
{
    //the new taxcode must have a name
    if (taxcode.name.isEmpty())
        return false;

    //that is not already in use
    if(std::find_if( taxcodes.begin(), taxcodes.end(), [&taxcode](const auto& pair) { return pair.second.name == taxcode.name; } ) != taxcodes.end())
        return false;

    return true;
}

bool AccountsData::taxcode_add(const CTaxcode taxcode, const Taxcode_Id id)
{
    return taxcode_can_add(taxcode) && taxcodes.insert(std::pair<Taxcode_Id, CTaxcode>(id, taxcode)).second;
}

bool AccountsData::taxcode_can_remove(Taxcode_Id id) const
{
    //the tax code must exist
    if (taxcodes.find(id) == taxcodes.end())
        return false;

    //the tax code must not be used by a catagory
    if (std::find_if(categories.begin(), categories.end(), [&id](const auto& pair) { return pair.second.taxcode_id == id; }) != categories.end())
        return false;

    return true;
}

bool AccountsData::taxcode_remove(Taxcode_Id id)
{
    return taxcode_can_remove(id) && (taxcodes.erase(id) == 1);
}

bool AccountsData::taxcode_can_replace(const Taxcode_Id id, const CTaxcode taxcode) const
{
    //the new tax code must have a name
    if (taxcode.name.isEmpty())
        return false;

    //the old tax code must exist
    if(taxcodes.find(id) == taxcodes.end())
        return false;

    //the new tax code must not be the same as the old tax code (No change)
    if (taxcodes.at(id) == taxcode)
        return false;

    //todo
    //the new tax code must not have the same name as an existing taxcode.
    //if(std::find(taxcodes.begin(), taxcodes.end(), [&taxcode](const auto& pair){ return pair.second.name == taxcode.name; }) != taxcodes.end())
    //    return false;

    return true;
}

bool AccountsData::taxcode_replace(Taxcode_Id id, CTaxcode taxcode)
{
    return taxcode_can_replace(id, taxcode)
     && (taxcodes.erase(id) == 1)
     && taxcodes.insert(std::pair<Taxcode_Id, CTaxcode>(id, taxcode)).second;
}

bool AccountsData::property_can_add(const CProperty property) const
{
    //the new property must have a name
    if (property.name.isEmpty())
        return false;

    //that is not already in use
    if(std::find_if( properties.begin(), properties.end(), [&property](const auto& pair) { return pair.second.name == property.name; } ) != properties.end())
        return false;

    return true;
}

bool AccountsData::property_add(const CProperty property, const Property_Id id)
{
    return property_can_add(property) && properties.insert(std::pair<Property_Id, CProperty>(id, property)).second;
}

bool AccountsData::property_can_remove(const Property_Id id) const
{
    //the property code must exist
    if (properties.find(id) == properties.end())
        return false;

    //the property code must not be used by an entry
    if (std::find_if(entries.begin(), entries.end(), [&id](const auto& pair) { return pair.second.property_id == id; }) != entries.end())
        return false;

    return true;
}

bool AccountsData::property_remove(Property_Id id)
{
    return property_can_remove(id) && (properties.erase(id) == 1);
}

bool AccountsData::property_can_replace(const Property_Id id, const CProperty property) const
{
    //the new property must have a name
    if (property.name.isEmpty())
        return false;

    //the old property must exist
    if (properties.find(id) == properties.end())
        return false;

    //the new property must not be the same as the old (No change)
    if (properties.at(id) == property)
        return false;

    //the new property must not have the same name as an existing property.
    const auto it = std::find_if(properties.begin(), properties.end(), [&property](const auto &pair){ return pair.second.name == property.name; });
    if (it == properties.end()

 == properties.end())

    //todo
    // //the new property must not have the same name as an existing property.
    // foreach(KeyValuePair<PropertyId, Property> pair in this._propertyList)
    // {
    //     if ((pair.Value.Name == property.Name) && (pair.Key != id)) return false;
    // }
    // return true;
}


bool AccountsData::property_replace(const Property_Id id, const CProperty property)
{
    return property_can_replace(id, property)
     && (properties.erase(id) == 1)
     && properties.insert(std::pair<Property_Id, CProperty>(id, property)).second;
}

bool AccountsData::payee_can_add(CPayee payee) const
{
    //the new payee must have a name
    if (payee.name.isEmpty())
        return false;

    //that is not already in use
    if(std::find_if( payees.begin(), payees.end(), [&payee](const auto& pair) { return pair.second.name == payee.name; } ) != payees.end())
        return false;

    return true;
}

bool AccountsData::payee_add(CPayee payee, Payee_Id id)
{
    return payee_can_add(payee) && payees.insert(std::pair<Payee_Id, CPayee>(id, payee)).second;
}

bool AccountsData::payee_can_remove(Payee_Id id) const
{
    //the payee must exist
    if (payees.find(id) == payees.end())
        return false;

    if (std::find_if(entries.begin(), entries.end(), [&id](const auto& pair){ return pair.second.payee_id == id; }) != entries.end())
        return false;

    return true;
}

bool AccountsData::payee_remove(const Payee_Id id)
{
    return payee_can_remove(id) && (payees.erase(id) == 1);
}

bool AccountsData::payee_can_replace(Payee_Id, CPayee) const
{
    //todo
    return true;
}

bool AccountsData::payee_replace(const Payee_Id id, const CPayee payee)
{
    return payee_can_replace(id, payee)
     && (payees.erase(id) == 1)
     && payees.insert(std::pair<Payee_Id, CPayee>(id, payee)).second;
}

bool AccountsData::category_can_add(CCategory category) const
{
    struct find_by_name
    {
        find_by_name(const QString& name) : name(name) {}
		bool operator()(const std::pair<Category_Id, CCategory>& pair) { return pair.second.name == name; }
    private:
        QString name;
    };

	auto it = std::find_if(categories.begin(), categories.end(), find_by_name(category.name));
	return !category.name.isEmpty() && (it == categories.end());
}

bool AccountsData::category_add(const CCategory category, const Category_Id id)
{
    return category_can_add(category) && categories.insert(std::pair<Category_Id, CCategory>(id, category)).second;
}

bool AccountsData::category_can_remove(Category_Id) const
{
    //todo
    return true;
}

bool AccountsData::category_remove(const Category_Id id)
{
    return category_can_remove(id) && (categories.erase(id) == 1);
}

bool AccountsData::category_can_replace(Category_Id, CCategory) const
{
    //todo
    return true;
}

bool AccountsData::category_replace(const Category_Id id, const CCategory category)
{
    return category_can_replace(id, category)
    && (categories.erase(id) == 1)
        && categories.insert(std::pair<Category_Id, CCategory>(id, category)).second;
}

bool AccountsData::account_can_add(CAccount acc) const
{
	auto it = std::find_if( accounts.begin(), accounts.end(), [&acc](const auto& pair) { return pair.second.name == acc.name; } );
	return !acc.name.isEmpty() && it == accounts.end();
}

bool AccountsData::account_add(CAccount account, Account_Id id)
{
    return account_can_add(account) && accounts.insert(std::pair<Account_Id, CAccount>(id, account)).second;
}

bool AccountsData::account_can_remove(Account_Id) const
{
    //todo
    return true;
}

bool AccountsData::account_remove(const Account_Id id)
{
    return account_can_remove(id) && (accounts.erase(id) == 1);
}

bool AccountsData::account_can_replace(Account_Id, CAccount) const
{
    //todo
    return true;
}

bool AccountsData::account_replace(const Account_Id id, CAccount account)
{
    return account_can_replace(id, account)
    && (accounts.erase(id) == 1)
        && accounts.insert(std::pair<Account_Id, CAccount>(id, account)).second;
}

bool AccountsData::entry_can_add(CEntry entry) const
{
	return true;
	std::vector<QString> reasons;

	if (entry.account_id.isNull())
	{
		reasons.push_back("No account specified");
	}
	else if (!accounts.count(entry.account_id))
	{
		reasons.push_back("Account does not exist");
	}

	if (reasons.size() > 0)
		return false;

	//locked or reconciled account reasons
	//CAccount account = accounts.at(entry.account_id);
	if (accounts.at(entry.account_id).lock == eAccLock::AccLock::Locked)
		reasons.push_back("Account is locked, ");
	else if ((accounts.at(entry.account_id).lock == eAccLock::AccLock::LockedUntil) && (entry.date < accounts.at(entry.account_id).locked_until))
		reasons.push_back("Account is locked on the given date, ");
	if (entry.date <= accounts.at(entry.account_id).reconciled_on)
		reasons.push_back("Account is reconciled on the given date");

	//if this is a transfer
	if(!entry.transfer_id.isNull())
	{
		if (!accounts.count(entry.transfer_id))
			reasons.push_back("Account does not exist");

		//locked or reconciled account reasons
		const CAccount account = accounts.at(entry.transfer_id);
		if (account.lock == eAccLock::AccLock::Locked)
			reasons.push_back("Account is locked, ");
		else if ((account.lock == eAccLock::AccLock::LockedUntil) && (entry.date < account.locked_until))
			reasons.push_back("Account is locked on the given date, ");
		if (entry.date <= account.reconciled_on)
			reasons.push_back("Account is reconciled on the given date");
	}

	return reasons.size() > 0 ? false : true;
}

bool AccountsData::entry_add(const CEntry entry, const Entry_Id id)
{
    return entry_can_add(entry) && entries.insert(std::pair<Entry_Id, CEntry>(id, entry)).second;
}

bool AccountsData::entry_can_remove(Entry_Id) const
{
    //todo
    return true;
}

bool AccountsData::entry_remove(const Entry_Id id)
{
    return entry_can_remove(id) && (entries.erase(id) == 1);
}

bool AccountsData::entry_can_replace(Entry_Id, CEntry) const
{
    //todo
    return true;
}

bool AccountsData::entry_replace(const Entry_Id id, const CEntry entry)
{
    return entry_can_replace(id, entry)
    && (entries.erase(id) == 1)
        && entries.insert(std::pair<Entry_Id, CEntry>(id, entry)).second;
}


