# Generated by Django 4.2.4 on 2023-10-28 19:52

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('grades', '0002_submission_assignment_alter_submission_author_and_more'),
    ]

    operations = [
        migrations.AlterField(
            model_name='submission',
            name='score',
            field=models.FloatField(null=True),
        ),
    ]