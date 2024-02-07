# Generated by Django 4.2.4 on 2023-10-28 19:51

from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('grades', '0001_initial'),
    ]

    operations = [
        migrations.AddField(
            model_name='submission',
            name='assignment',
            field=models.TextField(default='hw'),
        ),
        migrations.AlterField(
            model_name='submission',
            name='author',
            field=models.ForeignKey(on_delete=django.db.models.deletion.PROTECT, related_name='user_author', to=settings.AUTH_USER_MODEL),
        ),
        migrations.AlterField(
            model_name='submission',
            name='grader',
            field=models.ForeignKey(on_delete=django.db.models.deletion.PROTECT, related_name='user_grader', to=settings.AUTH_USER_MODEL),
        ),
    ]